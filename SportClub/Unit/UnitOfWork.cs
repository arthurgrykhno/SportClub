using SportClub.Repositories;

namespace SportClub.Unit
{
    public class UnitOfWork
    {
        public UnitOfWork()
        {
            this._connectionString = @"Data Source = (localdb)\MSSQLLocalDB; Initial Catalog = SportClub; Integrated Security = True;";
        }

        private readonly string _connectionString;
        private GroupRepository groupRepository;
        private ScheduleRepository scheduleRepository;
        private SubscriptionRepository subscriptionRepository;
        private TrainerRepository trainerRepository;
        private TrainingRepository trainingRepository;

        public GroupRepository Groups
        {
            get
            {
                if (groupRepository == null)
                {
                    groupRepository = new GroupRepository(_connectionString);
                }

                return groupRepository;
            }
        }

        public ScheduleRepository Schedules
        {
            get
            {
                if (scheduleRepository == null)
                {
                    scheduleRepository = new ScheduleRepository(_connectionString);
                }

                return scheduleRepository;
            }
        }

        public SubscriptionRepository Subscriptions
        {
            get
            {
                if (subscriptionRepository == null)
                {
                    subscriptionRepository = new SubscriptionRepository(_connectionString);
                }

                return subscriptionRepository;
            }
        }

        public TrainerRepository Trainers
        {
            get
            {
                if (trainerRepository == null)
                {
                    trainerRepository = new TrainerRepository(_connectionString);
                }

                return trainerRepository;
            }
        }

        public TrainingRepository Trainings
        {
            get
            {
                if (trainingRepository == null)
                {
                    trainingRepository = new TrainingRepository(_connectionString);
                }

                return trainingRepository;
            }
        }
    }
}
