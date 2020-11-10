using System;
using ClinicAppDAL.EF;
using ClinicAppDAL;
using ClinicAppDAL.Models.AuthModel ;

namespace Tset
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("***** Fun with ADO.NET EF Core 2 *****\n");

           /* using (ClinicAppAuthContext context = new ClinicAppAuthContext())
            {
                ClinicAppDAL.DataInitiaze.DataInitiaizer.CreateDb(context);
                ClinicAppDAL.DataInitiaze.DataInitiaizer.InitialiseAdmin(context);
                foreach (User c in context.Users)
                {
                    Console.WriteLine(c.Password);
                }
            }*/
              Console.WriteLine("***** Using a Repository *****\n");
              using (var repo = new ClinicAppDAL.Repos.AuthRepo.UserRepo(new ClinicAppAuthContext()))
              {
                AddNewRecord(new User
                {
                    Login = "test",
                    Password = "test",
                    Role = 1,
                    Access = true
                }); ;
                  foreach (User c in repo.GetAll())
                  {
                      Console.WriteLine(c.Password);
                  }
              }
              //TestConcurrency();
              Console.ReadLine();
          }
        
          private static void AddNewRecord(User car)
          {
              using (var repo = new ClinicAppDAL.Repos.AuthRepo.UserRepo(new ClinicAppAuthContext()))
              {
                  repo.Add(car);
              }
          }
        /*
          private static void UpdateRecord(int carId)
          {
              using (var repo = new InventoryRepo())
              {
                  // Grab the car, change it, save! 
                  var carToUpdate = repo.GetOne(carId);
                  if (carToUpdate == null) return;
                  carToUpdate.Color = "Blue";
                  repo.Update(carToUpdate);
              }
          }

          private static void RemoveRecordByCar(Inventory carToDelete)
          {
              using (var repo = new InventoryRepo())
              {
                  repo.Delete(carToDelete);
              }
          }

          private static void RemoveRecordById(int carId, byte[] timeStamp)
          {
              using (var repo = new InventoryRepo())
              {
                  repo.Delete(carId, timeStamp);
              }
          }

          private static void TestConcurrency()
          {
              var repo1 = new InventoryRepo();
              //Use a second repo to make sure using a different context
              var repo2 = new InventoryRepo();
              var car1 = repo1.GetOne(1);
              var car2 = repo2.GetOne(1);
              car1.PetName = "NewName";
              repo1.Update(car1);
              car2.PetName = "OtherName";
              try
              {
                  repo2.Update(car2);
              }
              catch (DbUpdateConcurrencyException ex)
              {
                  var entry = ex.Entries.Single();
                  var currentValues = entry.CurrentValues;
                  var originalValues = entry.OriginalValues;
                  var dbValues = entry.GetDatabaseValues();
                  Console.WriteLine(" ******** Concurrency ************");
                  Console.WriteLine("Type\tPetName");
                  Console.WriteLine($"Current:\t{currentValues[nameof(Inventory.PetName)]}");
                  Console.WriteLine($"Orig:\t{originalValues[nameof(Inventory.PetName)]}");
                  Console.WriteLine($"db:\t{dbValues[nameof(Inventory.PetName)]}");
              }
          }
      }*/
        }
    }
