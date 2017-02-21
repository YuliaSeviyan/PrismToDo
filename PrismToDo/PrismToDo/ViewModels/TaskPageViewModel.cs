using Prism.Mvvm;
using Prism.Navigation;
using PrismToDo.Model;
using System;

namespace PrismToDo.ViewModels
{
    public class TaskPageViewModel : BindableBase, INavigationAware
    {
        private string _titlePage;
        public string TitlePage
        {
            get { return _titlePage; }
            set { SetProperty(ref _titlePage, value); }
        }

        private TaskModel _task;
        public TaskModel Task
        {
            get { return _task; }
            set { SetProperty(ref _task, value); }
        }
        
        public bool IsValid
        {
            get
            {
                return (!string.IsNullOrEmpty(_task.Title));
                /*
                return 
                    (
                     (!string.IsNullOrEmpty(_task.Title))
                     &&
                     (!string.IsNullOrEmpty(_task.Description))
                    );
                */
            }
        }

        private MainPageViewModel _listViewModel;
        public MainPageViewModel ListViewModel
        {
            get { return _listViewModel; }
            set { SetProperty(ref _listViewModel, value); }
        }
        
        public TaskPageViewModel()
        {
            Task = new TaskModel();
            Task.Date = DateTime.Today;            
        }

        public void OnNavigatedFrom(NavigationParameters parameters)
        {
            
        }

        public void OnNavigatedTo(NavigationParameters parameters)
        {
            if (parameters.ContainsKey("taskPage"))
            {
                TitlePage = (string)parameters["taskPage"];
            }
            if (parameters.ContainsKey("selectedTask"))
            {
                Task = (TaskModel)parameters["selectedTask"];                
            }            
            if (parameters.ContainsKey("listViewModel"))
            {
                ListViewModel = (MainPageViewModel)parameters["listViewModel"];
            }
        }

        public void OnNavigatingTo(NavigationParameters parameters)
        {

        }
    }
}
