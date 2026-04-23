using NEFAB.Domains;
using NEFAB.Repositories;
using NEFAB.Repositories.Interfaces;
using System;
using System.ComponentModel;

namespace NEFAB.Services
{
    public class EmployeeService
    {
      //  private readonly IRepoGetAddUpdate<Employee, string> _employeeRepository;

        public EmployeeRepository EmployeeRepository { get; set; }

        public EmployeeService()
        {
            EmployeeRepository = new EmployeeRepository();
        }

        public void Add(Employee employee)
        {
            if (string.IsNullOrEmpty(employee.EmployeeID) || (employee.EmployeeName == null))
            {
                throw new ArgumentException("Udfyld venligst både EmployeeID og EmployeeName.");
            }


            if (employee.EmployeeID.Length != 8)
            {
                throw new ArgumentException("EmployeeID skal være præcis 8 tegn langt.");
            }

            for (int i = 0; i < employee.EmployeeID.Length; i++)
            {
                if (i <= 3)
                {
                    char e = employee.EmployeeID[i];
                    if (!(e >= 'A' && e <= 'Z'))
                    {
                        throw new ArgumentException("De første 4 tegn i EmployeeID skal være store bogstaver (A-Z).");
                    }
                }
                else
                {
                    if (!char.IsDigit(employee.EmployeeID[i]))
                        throw new ArgumentException("De sidste 4 tegn skal være tal (0-9).");
                }
            }

                try
                {
                    EmployeeRepository.Add(employee);

                }
                catch
                {
                    throw new ArgumentException("Noget gik galt, prøv igen");
                }

            }
        
             
            
        

        public void Update(Employee employee)
        {
            if (string.IsNullOrEmpty(employee.EmployeeID) || employee.EmployeeName == null)
            {
                throw new ArgumentException("Udfyld venligst både EmployeeID, Nyt EmployeeID og EmployeeName.");
            }

            if (employee.EmployeeID.Length != 8)
            {
                throw new ArgumentException("Udfyld venligst et korrekt employeeID");
            }

            for (int i = 0; i < employee.EmployeeID.Length; i++)
            {
                if (i <= 3)
                {
                    char e = employee.EmployeeID[i];
                    if (!(e >= 'A' && e <= 'Z'))
                    {
                        throw new ArgumentException("EmployeeID er ikke af korrekt type: ABCD1234");
                    }

                }
                else
                {

                    if (!char.IsDigit(employee.EmployeeID[i]))
                    {
                        throw new ArgumentException("EmployeeID er ikke af korrekt type: ABCD1234");
                    }
                }


                    Employee? employeeDB = EmployeeRepository.GetByID(employee.EmployeeID);
                    if (employeeDB == null)
                    {
                        throw new Exception($"Employee {employee.EmployeeID} blev ikke fundet.");
                    }

                        try
                        {
                            EmployeeRepository.Update(employee);
                        }
                        catch (Exception ex)
                        {
                            throw new Exception("Employee kunne ikke opdateres");
                        }
                    }
                }

            }
        }
    
