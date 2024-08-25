namespace BasicCompany
{
    public class Company
    {
        public string Name { get; set; }
        public string Id { get; set; }

        protected internal string CoWorkerCard { get; set; }

        private static HashSet<Visitor> invitedVisitors = new HashSet<Visitor>();
        private static Dictionary<Visitor, Company> visitorInvitedBy = new Dictionary<Visitor, Company>();

        public virtual void LeftFromCompany(List<Company> workerList)
        {
            if (workerList.Contains(this))
            {
                workerList.Remove(this);
                Console.WriteLine($"{Name} has left the company.");
            }
            else
            {
                Console.WriteLine($"{Name} is not part of the company and cannot leave.");
            }
        }

        public virtual void JoinInCompany(List<Company> workerList)
        {
            if (!workerList.Contains(this))
            {
                workerList.Add(this);
                Console.WriteLine($"{Name} has joined the company.");
            }
            else
            {
                Console.WriteLine($"{Name} is already part of the company.");
            }
        }

        public void InviteVisitor(Visitor visitor)
        {
            if (this is CoWorker || this is Manager || this is Specialist || this is BEDeveloper || this is Operator)
            {
                if (!invitedVisitors.Contains(visitor))
                {
                    invitedVisitors.Add(visitor);
                    visitorInvitedBy[visitor] = this;
                    Console.WriteLine($"{Name} invited {visitor.Name} to the company.");
                }
                else
                {
                    Console.WriteLine($"{visitor.Name} has already been invited.");
                }
            }
            else
            {
                Console.WriteLine($"{Name} is not authorized to invite visitors.");
            }
        }

        public bool AllowVisitorToJoin(Visitor visitor, List<Company> companyList)
        {
            if (invitedVisitors.Contains(visitor))
            {
                visitor.JoinInCompany(companyList);
                invitedVisitors.Remove(visitor);
                return true;
            }
            else
            {
                Console.WriteLine($"{visitor.Name} was not invited or is not authorized to join.");
                return false;
            }
        }
    }

    public class CoWorker : Company { }

    public class Manager : Company
    {
        public override void JoinInCompany(List<Company> workerList)
        {
            base.JoinInCompany(workerList);
        }
    }

    public class Specialist : Company
    {
        public TimeSpan WorkStart { get; set; }
        public TimeSpan WorkEnd { get; set; }

        public Specialist()
        {
            WorkStart = new TimeSpan(9, 0, 0);
            WorkEnd = new TimeSpan(17, 0, 0);
        }

        public Specialist(TimeSpan workStart, TimeSpan workEnd)
        {
            WorkStart = workStart;
            WorkEnd = workEnd;
        }

        public bool IsWorking(TimeSpan time)
        {
            return time >= WorkStart && time <= WorkEnd;
        }

        public override void JoinInCompany(List<Company> workerList)
        {
            base.JoinInCompany(workerList);
            Console.WriteLine($"{Name} works from {WorkStart} to {WorkEnd}.");
        }
    }

    public class BEDeveloper : Specialist { }
    public class Operator : Specialist { }

    public class Visitor : Company
    {
        public new string CoWorkerCard
        {
            get
            {
                throw new InvalidOperationException("Visitor class cannot access CoWorkerCard.");
            }
            set
            {
                throw new InvalidOperationException("Visitor class cannot access CoWorkerCard.");
            }
        }

        public override void JoinInCompany(List<Company> workerList)
        {
            Console.WriteLine("Visitors cannot join the company directly. They need an invite.");
        }

        public override void LeftFromCompany(List<Company> workerList)
        {
            if (workerList.Contains(this))
            {
                base.LeftFromCompany(workerList);
            }
            else
            {
                Console.WriteLine($"{Name} cannot leave the company because they are not a part of it.");
            }
        }
    }
}