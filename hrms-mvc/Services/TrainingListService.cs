using hrms_mvc.Data;
using hrms_mvc.Models;
using hrms_mvc.Repository;
using Microsoft.EntityFrameworkCore;

namespace hrms_mvc.Services
{
    public class TrainingListService : ITrainingListService
    {
        private readonly AppDbContext db;

        public TrainingListService(AppDbContext db)
        {
            this.db = db;
        }

        public async Task AddTrainingList(TrainingListModal trainingList)
        {
            var data = new Traininglist
            {
                TrainersId = trainingList.TrainersId,
                TrainingTypeId = trainingList.TrainingTypeId,
                UserId = trainingList.UserId,
                TrainingCost = trainingList.TrainingCost,
                Description = trainingList.Description,
                Status = trainingList.Status,
                StartDate = trainingList.StartDate,
                EndDate = trainingList.EndDate,
                CreatedAt = DateTime.Now,
                CreatedBy = "Admin",
                ModifiedBy = "Admin",
                ModifiedAt = DateTime.Now
            };

            await db.Traininglist.AddAsync(data);
            await db.SaveChangesAsync();
        }

        public async Task DeleteTrainingList(int id)
        {
            var temp = await db.Traininglist.FindAsync(id);

            if (temp != null)
            {
                db.Traininglist.Remove(temp);
                await db.SaveChangesAsync();
            }
        }

        public async Task<List<Traininglist>> FetchAllTrainingLists()
        {
            var temp = await db.Traininglist
                .Include(x => x.Trainers)
                .Include(x => x.TrainingType)
                .Include(x => x.User)
                .ToListAsync();

            return temp;
        }

        public async Task<TrainingListModal> FindTrainingListByID(int id)
        {
            var temp = await db.Traininglist.FindAsync(id);

            if (temp == null)
            {
                return null;
            }

            var data = new TrainingListModal
            {
                TrainingId = temp.TrainingId,
                TrainersId = temp.TrainersId,
                TrainingTypeId = temp.TrainingTypeId,
                UserId = temp.UserId,
                TrainingCost = temp.TrainingCost,
                Description = temp.Description,
                Status = temp.Status,
                StartDate = temp.StartDate,
                EndDate = temp.EndDate
            };

            return data;
        }

        public async Task UpdateTrainingList(TrainingListModal trainingList)
        {
            var data = await db.Traininglist.FindAsync(trainingList.TrainingId);

            if (data != null)
            {
                data.TrainersId = trainingList.TrainersId;
                data.TrainingTypeId = trainingList.TrainingTypeId;
                data.UserId = trainingList.UserId;
                data.TrainingCost = trainingList.TrainingCost;
                data.Description = trainingList.Description;
                data.Status = trainingList.Status;
                data.StartDate = trainingList.StartDate;
                data.EndDate = trainingList.EndDate;
                data.ModifiedBy = "Admin";
                data.ModifiedAt = DateTime.Now;

                await db.SaveChangesAsync();
            }
        }
    }
}