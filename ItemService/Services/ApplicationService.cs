using ItemService.Dto;
using ItemService.Models;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace ItemService.Services
{
    public class ApplicationService : IApplicationService
    {
        private readonly ApplicationDbContext _context;

        public ApplicationService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<int> CreateApplication(ApplicationCreateDto appl)
        {
            var newAppl = new Application
            {
                OwnerUserId = appl.OwnerUserId,
                OwnerName = appl.OwnerName,
                ApplicationName = appl.ApplicationName,
                ApplicationDescription = appl.ApplicationDescription,
                State = ApplicationState.New
            };

            await _context.Applications.AddAsync(newAppl);
            await _context.SaveChangesAsync();

            foreach (var item in appl.ItemsInApplication)
            {
                var newApplItem = new ApplicationItems
                {
                    ApplicationId = newAppl.Id,
                    ItemId = item.ItemId,
                    OwnerId = item.OwnerId,
                    DateTimeStart = item.DateTimeStart,
                    DateTimeEnd = item.DateTimeEnd,
                    State = ApplicationItemStates.WaitApprove
                };

                await _context.ApplicationsItems.AddAsync(newApplItem);
                await _context.SaveChangesAsync();
            }

            return newAppl.Id;
        }

        public List<ApplicationItems> GetAllItemsInAppl(int applId)
        {
            return _context.ApplicationsItems.Where(c => c.ApplicationId == applId).ToList();
        }

        public List<ApplicationItems> GetAllItemsInApplByUser(int applId, int UserId)
        {
            return _context.ApplicationsItems.Where(c => c.ApplicationId == applId && c.OwnerId == UserId).ToList();
        }

        public GetApplDto GetApplication(int applId)
        {
            var appl = _context.Applications.FirstOrDefault(c => c.Id == applId);

            if (appl == null)
            {
                throw new ValidationException("This application does not exist");
            }

            var application = new GetApplDto
            {
                Id = appl.Id,
                OwnerUserId = appl.OwnerUserId,
                OwnerName = appl.OwnerName,
                ApplicationName = appl.ApplicationName,
                ApplicationDescription = appl.ApplicationDescription,
                State = appl.State,
                ItemsInApplication = GetAllItemsInAppl(applId)
            };
            return application;
        }

        public List<GetApplDto> GetApplicationsOwnedByUser(int UserId)
        {
            var appls = _context.Applications.Where(c => c.OwnerUserId == UserId).ToList();

            var applications = new List<GetApplDto>();
            foreach (var appl in appls)
            {
                var application = new GetApplDto
                {
                    Id = appl.Id,
                    OwnerUserId = appl.OwnerUserId,
                    OwnerName = appl.OwnerName,
                    ApplicationName = appl.ApplicationName,
                    ApplicationDescription = appl.ApplicationDescription,
                    State = appl.State,
                    ItemsInApplication = GetAllItemsInAppl(appl.Id)
                };
                applications.Add(application);
            }
            return applications;
        }

        public List<GetApplDto> GetIncomingApplications(int UserId)
        {
            var itemsInAppls = _context.ApplicationsItems.Where(c => c.OwnerId == UserId).ToList();
            var applications = new List<GetApplDto>();
            foreach (var item in itemsInAppls)
            {
                var appl = _context.Applications.FirstOrDefault(c => c.Id == item.ApplicationId);
                var application = new GetApplDto
                {
                    Id = appl.Id,
                    OwnerUserId = appl.OwnerUserId,
                    OwnerName = appl.OwnerName,
                    ApplicationName = appl.ApplicationName,
                    ApplicationDescription = appl.ApplicationDescription,
                    State = appl.State,
                    ItemsInApplication = GetAllItemsInApplByUser(appl.Id, UserId)
                };
                applications.Add(application);
            }
            return applications;
        }

        public async Task UpdateApplication(int id, ApplicationUpdateDto model)
        {
            var appl = _context.Applications.FirstOrDefault(c => c.Id == id);

            if (appl == null)
            {
                throw new ValidationException("This application does not exist");
            }

            appl.ApplicationName = model.ApplicationName;
            appl.ApplicationDescription = model.ApplicationDescription;

            await _context.SaveChangesAsync();
            
            foreach (var item in model.ItemsInApplication)
            {
                var itemInAppl = _context.ApplicationsItems.FirstOrDefault(c => (c.ApplicationId == id && c.ItemId == item.ItemId));

                if (itemInAppl == null)
                {
                    var newApplItem = new ApplicationItems
                    {
                        ApplicationId = id,
                        ItemId = item.ItemId,
                        OwnerId = item.OwnerId,
                        DateTimeStart = item.DateTimeStart,
                        DateTimeEnd = item.DateTimeEnd,
                        State = ApplicationItemStates.WaitApprove
                    };

                    await _context.ApplicationsItems.AddAsync(newApplItem);
                    await _context.SaveChangesAsync();
                }
                else
                {
                    itemInAppl.DateTimeStart = item.DateTimeStart;
                    itemInAppl.DateTimeEnd = item.DateTimeEnd;
                    await _context.SaveChangesAsync();
                }
            }
        }

        public async Task DeleteApplication(int id)
        {
            var appl = _context.Applications.FirstOrDefault(c => c.Id == id);

            if (appl == null)
            {
                throw new ValidationException("This application does not exist");
            }

            var itemsInAppl = _context.ApplicationsItems.Where(c => c.ApplicationId == id).ToList();

            foreach (var item in itemsInAppl)
            {
                _context.ApplicationsItems.Remove(item);
                await _context.SaveChangesAsync();
            }

            _context.Applications.Remove(appl);
            await _context.SaveChangesAsync();
        }

        public async Task ChangeItemInApplState(ApplicationItemStates state, int id)
        {
            var itemInAppl = _context.ApplicationsItems.FirstOrDefault(c => c.Id == id);

            if (itemInAppl == null)
            {
                throw new ValidationException("This item does not exist in application");
            }

            itemInAppl.State = state;

            await _context.SaveChangesAsync();

            //логика по смене состояний основной заявки
            var listItems = new List<ApplicationItems>();
            listItems = GetAllItemsInAppl(itemInAppl.ApplicationId); //все услуги в текущей заявке
            int cnt = 0;
            var application = _context.Applications.FirstOrDefault(c => c.Id == itemInAppl.ApplicationId);
            if (application == null)
            {
                throw new ValidationException("This item does not exist in application");
            }
            foreach (var item in listItems)
            {
                if (item.State == ApplicationItemStates.Approved) cnt++;
            }

            if (state == ApplicationItemStates.Approved)
            {   
                if (cnt == listItems.Count) //если все заявки согласованы
                {
                    application.State = ApplicationState.Approved;
                    await _context.SaveChangesAsync();
                } else if (cnt == 1) //если согласована первая заявка
                {
                    application.State = ApplicationState.InProgress;
                    await _context.SaveChangesAsync();
                }
            } else if (state == ApplicationItemStates.Declined) //если кто-то отменил участие, то заявка в процессе (мб она была согласована, но пришлось откатить)
            {
                application.State = ApplicationState.InProgress;
                await _context.SaveChangesAsync();
            }
        }

        public async Task ChangeTimesItemInApplState(ResReservTimeDto times, int id)
        {
            var itemInAppl = _context.ApplicationsItems.FirstOrDefault(c => c.Id == id);

            if (itemInAppl == null)
            {
                throw new ValidationException("This item does not exist in application");
            }

            itemInAppl.State = ApplicationItemStates.Approved;
            itemInAppl.DateTimeStart = times.Start;
            itemInAppl.DateTimeEnd = times.End;

            await _context.SaveChangesAsync();

            var listItems = new List<ApplicationItems>();
            listItems = GetAllItemsInAppl(itemInAppl.ApplicationId); //все услуги в текущей заявке
            int cnt = 0;
            var application = _context.Applications.FirstOrDefault(c => c.Id == itemInAppl.ApplicationId);
            if (application == null)
            {
                throw new ValidationException("This item does not exist in application");
            }
            foreach (var item in listItems)
            {
                if (item.State == ApplicationItemStates.Approved) cnt++;
            }
            if (cnt == listItems.Count) //если все заявки согласованы
            {
                application.State = ApplicationState.Approved;
                await _context.SaveChangesAsync();
            }
            else if (cnt == 1) //если согласована первая заявка
            {
                application.State = ApplicationState.InProgress;
                await _context.SaveChangesAsync();
            }
        }
    }
}
