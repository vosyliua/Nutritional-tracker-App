using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using NutritionApp.Models;
using NutritionApp.Services;
using System.Linq;
using System.Collections.Generic;
using NutritionApp.Views;
using System.ComponentModel;

namespace NutritionApp.ViewModels
{
    internal class BackLogsPageViewModel : BaseViewModel
    {
        private ObservableCollection<Fixed> _backlogs;
        public string test { get; set; }


        public List<Backlog> _listBacklog {get;set;}
        public List<Backlog> ListBacklog
        {
            get => _listBacklog;
            set
            {
                _listBacklog = value;
                OnPropertyChanged(nameof(ListBacklog));
            }
        }
        public ObservableCollection<Fixed> Backlogs
        {
            get => _backlogs;
            set
            {
                _backlogs = value;
                OnPropertyChanged(nameof(Backlogs));
            }
        }

        public IBacklogService1 _backlogService;
        public IAccountService _accountService;
        public BackLogsPageViewModel(IBacklogService1 backlogService, IAccountService accountService)
        {
            _backlogService = backlogService;
            _accountService = accountService;
            Backlogs = new ObservableCollection<Fixed>();
            ListBacklog = new List<Backlog>();
            Console.WriteLine("Initialization1");
            
        }

        
        public async Task Send()
        {
            foreach(Backlog item in ListBacklog)
            {
                Console.WriteLine(item.Date);
            }
            if(ListBacklog.Count!= 0)
            {
                MessagingCenter.Send<BackLogsPageViewModel, List<Backlog>>(this, "tack", ListBacklog);
            }
            
        }

        public async Task Initialise()
        {
            Console.WriteLine("in initialise function backlogpage");
            var username = _accountService.getUsername();
            Console.WriteLine(username);
            ListBacklog = await _backlogService.returnData(username);
            Console.WriteLine(ListBacklog.Count);

        }

    }
}
