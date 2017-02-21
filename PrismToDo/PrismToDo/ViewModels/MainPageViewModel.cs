using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System.Collections.ObjectModel;

namespace PrismToDo.ViewModels
{
    public class MainPageViewModel : BindableBase, INavigationAware
    {
        private const string DATABASE_NAME = "tasks.db";
        private readonly TaskRepository _repository;

        private readonly INavigationService _navigationService;

        private ObservableCollection<TaskPageViewModel> _tasks;       
        public ObservableCollection<TaskPageViewModel> Tasks
        {
            get { return _tasks; }
            set { SetProperty(ref _tasks, value); }
        }

        private TaskPageViewModel _selectedTask;
        public TaskPageViewModel SelectedTask
        {
            get { return _selectedTask; }
            set
            {
                SetProperty(ref _selectedTask, value);
                SelectTask(_selectedTask);
            }           
        }

        private DelegateCommand _createTaskCommand;
        public DelegateCommand CreateTaskCommand
        {
            get
            {
                if (_createTaskCommand == null)
                {
                    _createTaskCommand = new DelegateCommand(this.CreateTask);
                }
                return _createTaskCommand;
            }
        }
        
        private DelegateCommand<TaskPageViewModel> _saveTaskCommand;
        public DelegateCommand<TaskPageViewModel> SaveTaskCommand
        {
            get
            {
                if (_saveTaskCommand == null)
                {
                    _saveTaskCommand = new DelegateCommand<TaskPageViewModel>(this.SaveTask);
                }
                return _saveTaskCommand;
            }
        }

        private DelegateCommand<TaskPageViewModel> _deleteTaskCommand;
        public DelegateCommand<TaskPageViewModel> DeleteTaskCommand
        {
            get
            {
                if (_deleteTaskCommand == null)
                {
                    _deleteTaskCommand = new DelegateCommand<TaskPageViewModel>(this.DeleteTask);
                }
                return _deleteTaskCommand;
            }
        }
        
        public MainPageViewModel(INavigationService navigationService)
        {
            Tasks = new ObservableCollection<TaskPageViewModel>();
            _navigationService = navigationService;
            _repository = new TaskRepository(DATABASE_NAME);
        }

        private async void CreateTask()
        {
            var param = new NavigationParameters();
            param.Add("listViewModel", this);
            param.Add("taskPage", "New Task");
            await _navigationService.NavigateAsync("TaskPage", param);            
        }

        private async void SelectTask(TaskPageViewModel taskPageVM)
        {
            var param = new NavigationParameters();
            param.Add("selectedTask", taskPageVM.Task);
            param.Add("listViewModel", this);
            param.Add("taskPage", "Task");
            await _navigationService.NavigateAsync("TaskPage", param);           
        }

        private void GetTasks()
        {
            Tasks.Clear();

            var temp = _repository.GetItems();
                        
            foreach (var i in temp)
            {
                Tasks.Add(new TaskPageViewModel { Task = i, ListViewModel = this });
            }
        }
        private void SaveTask(TaskPageViewModel taskPageVM)
        {
            if (taskPageVM != null && taskPageVM.IsValid)
            {
                _repository.SaveItem(taskPageVM.Task);
                GetTasks();
                Back();
            }                                 
        }

        private void DeleteTask(TaskPageViewModel taskPageVM)
        {
            if (taskPageVM != null)
            {                
                _repository.DeleteItem(taskPageVM.Task.Id);
                GetTasks();                               
            }
            Back();
        }

        private async void Back()
        {
            await _navigationService.GoBackAsync();
        }

        public void OnNavigatedFrom(NavigationParameters parameters)
        {

        }

        public void OnNavigatedTo(NavigationParameters parameters)
        {
           
        }

        public void OnNavigatingTo(NavigationParameters parameters)
        {
            GetTasks();
        }
    }
}
