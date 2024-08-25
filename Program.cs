using System;
using System.Collections.Generic;
using BasicCompany;

namespace myProject
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Company> workersList = new List<Company>();

            Console.WriteLine("How many co-workers do you want to add to the company?");
            if (!int.TryParse(Console.ReadLine(), out int NumberOfCoWorkers) || NumberOfCoWorkers <= 0)
            {
                Console.WriteLine("Invalid number. Exiting program.");
                return;
            }

            for (int i = 1; i <= NumberOfCoWorkers; i++)
            {
                workersList.Add(GetWorkerFromInput());                          //workerList (სია) - ში დინამიურად ვამატებ თანამშრომლებს, მომხმარებლის მიერ მითითებული რაოდენობით.
            }

            Visitor visitor = new Visitor { Name = "Guest" };
            if (!visitor.AllowVisitorToJoin(visitor, workersList))
            {
                Console.WriteLine("Visitor cannot join directly. They need an invite.");
            }

            Console.WriteLine("Who do you want to invite the visitor from?");
            Console.WriteLine("Choose a number corresponding to a worker you added:");

            int invitedFrom;
            while (true)
            {
                if (!int.TryParse(Console.ReadLine(), out invitedFrom) || invitedFrom <= 0 || invitedFrom > workersList.Count)          // მომხმარებელი უთითებს მის მიერ შერჩეული თანამშრომლებიდან ვის მოაწვევინებს ვიზიტორს.
                {
                    Console.WriteLine("Invalid choice. Please choose a valid worker index.");                                           // მხოლოდ კომპანიაში დასაქმებულ ადამიანს შეუძლია ვიზიტორის მოწვევა.
                    continue;
                }
                invitedFrom -= 1;       
                break;
            }

            Console.WriteLine("Attempting to invite a visitor...");
            workersList[invitedFrom].InviteVisitor(visitor);                        

            if (visitor.AllowVisitorToJoin(visitor, workersList))
            {
                workersList.Add(visitor);
                Console.WriteLine($"{visitor.Name} has successfully joined the company.");
            }
            else
            {
                Console.WriteLine($"{visitor.Name} could not join the company.");
            }

            Console.WriteLine("\nFinal list of workers in the company:");
            foreach (Company worker in workersList)
            {
                Console.WriteLine(worker.Name);
            }

            Console.WriteLine("Let's reate several objects:");


            BEDeveloper beDeveloper = new BEDeveloper { Name = "David" };                                                   //ვქმნით  რამდენიმე ობიექტს
            beDeveloper.JoinInCompany(workersList);
            beDeveloper.LeftFromCompany(workersList);

            Manager manager1 = new Manager();
            Console.WriteLine("Input name");
            manager1.Name = Console.ReadLine();
            manager1.JoinInCompany(workersList);

            Operator operator1 = new Operator();
            Console.WriteLine(" Input name");
            operator1.Name = Console.ReadLine();
            operator1.JoinInCompany(workersList);

            Specialist specialist1 = new Specialist();
            Console.WriteLine("Input name");
            specialist1.Name = Console.ReadLine();
            specialist1.JoinInCompany(workersList);
      


            Visitor visitor1 = new Visitor();
            visitor1.JoinInCompany(workersList);

            Console.WriteLine("The End");
            Console.WriteLine("Press any key to exit program");
            Console.ReadKey();
        }

        static Company GetWorkerFromInput()
        {
            while (true)
            {
                Console.WriteLine("[1] - Manager\n[2] - CoWorker\n[3] - Specialist\n[4] - BEDeveloper\n[5] - Operator");
                if (!int.TryParse(Console.ReadLine(), out int choice))
                {
                    Console.WriteLine("Invalid input. Please enter a number.");
                    continue;
                }

                switch (choice)
                {
                    case 1:
                        return new Manager { Name = GetString("Enter Manager Name") };
                    case 2:
                        return new CoWorker { Name = GetString("Enter CoWorker Name") };
                    case 3:
                        return new Specialist { Name = GetString("Enter Specialist Name") };
                    case 4:
                        return new BEDeveloper { Name = GetString("Enter BEDeveloper Name") };
                    case 5:
                        return new Operator { Name = GetString("Enter Operator Name") };
                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }
            }
        }

        static string GetString(string prompt)
        {
            Console.WriteLine("{0} > ", prompt);
            return Console.ReadLine();
        }
    }
}