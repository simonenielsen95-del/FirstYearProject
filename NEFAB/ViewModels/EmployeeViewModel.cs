using NEFAB.Commands;
using NEFAB.Domains;
using NEFAB.Repositories;
using NEFAB.Repositories.Interfaces;
using NEFAB.Services;
using NEFAB.Stores;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace NEFAB.ViewModels
{
    public class EmployeeViewModel : BaseViewModel
    {
        public ICommand NavigateToHomeViewCommand { get; }

        public ICommand CreateEmployeeCommand { get; }
        public ICommand UpdateEmployeeCommand { get; }

        private readonly EmployeeService _employeeService;

        private Employee _selectedCreateEmployee;
        public Employee SelectedCreateEmployee
        {
            get { return _selectedCreateEmployee; }
            set { _selectedCreateEmployee = value; OnPropertyChanged(); }
        }
        private Employee _selectedUpdateEmployee;
        public Employee SelectedUpdateEmployee
        {
            get { return _selectedUpdateEmployee; }
            set { _selectedUpdateEmployee = value; OnPropertyChanged(); }
        }

        public EmployeeViewModel(NavigationStore navigationStore)
        {
            NavigationService homenavigationService = new NavigationService(navigationStore, () => new HomeViewModel(navigationStore));

            NavigateToHomeViewCommand = new NavigateCommand(homenavigationService);

            CreateEmployeeCommand = new CommandHandler(() => CreateEmployee());
            UpdateEmployeeCommand = new CommandHandler(() => UpdateEmployee());

            _employeeService = new EmployeeService();

            SelectedCreateEmployee = new Employee();
            SelectedUpdateEmployee = new Employee();

        }

        public void CreateEmployee()
        {
            try
            {
                _employeeService.Add(SelectedCreateEmployee);
                MessageBox.Show("Ny Employee er blevet oprettet!", "Succes", MessageBoxButton.OK);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Employee kunne ikke oprettes! {ex}", "Fejl", MessageBoxButton.OK);
            }
        }

        public void UpdateEmployee()
        {
            try
            {
                _employeeService.Update(SelectedUpdateEmployee);
                MessageBox.Show("Ny Employee er belvet oprettet!", "Succes", MessageBoxButton.OK);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Employee kunne ikke oprettes! {ex}", "Fejl", MessageBoxButton.OK);
            }
        }
    }
}