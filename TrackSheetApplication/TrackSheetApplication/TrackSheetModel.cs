using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace TrackSheetApplication
{
    public class TrackSheetModel : INotifyPropertyChanged
    {
        Dictionary<string, string> performance = new Dictionary<string, string>();
        public TrackSheetModel()
        {
            var _allStudents = ReadStudentDetail();
            if (_allStudents != null)
            {
                var listobj = _allStudents.Select(s => s.Name).ToList();
                _students = new ObservableCollection<string>(listobj);
            }

            var _allWeeks = ReadWeekDesc();
            if (_allWeeks != null)
            {
                var listobj = _allWeeks.Select(s => s.WK).ToList();
                _weeks = new ObservableCollection<string>(listobj);
            }

            //0-he 1-his
            performance.Add("Good", "{0} has the good learning ability. {1} communication is good.Hard working in nature. {0} is inquisitive in grasping new things.overall {0} is doing great");
            performance.Add("Okay", "{0} has the learning ability. {1} communication is okay need to work on that. {0} has understanding of tasks and able to explain it. Hard working in nature. overall {0} is Okey");
            performance.Add("Bad", "{0} has the  Good learning ability.{1} communication is good. {0} is proactive in the class and {0} is doing job so unable to submit some assignments on time but {0} is working very hard. {0} is doing good.");
            performance.Add("Poor", "{0} need to improve {1} learning ability. {1} communication is okay need to work  hard on it.He is humble.performance is not satisfied");

        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        private ObservableCollection<string> _students;

        public ObservableCollection<string> Students
        {
            get
            {
                return _students;
            }
            set
            {
                _students = value;
                OnPropertyChanged("Students");
            }
        }

        private ObservableCollection<string> _weeks;

        public ObservableCollection<string> Weeks
        {
            get
            {
                return _weeks;
            }
            set
            {
                Weeks = value;
                OnPropertyChanged("Weeks");
            }
        }

        private string _sWeeks;
        public string SWeeks 
        {
            get => _sWeeks;
            set
            {
                _sWeeks = value;
                var _allWeeks = ReadWeekDesc();
                var desc=_allWeeks.Where(w => w.WK == _sWeeks).FirstOrDefault().Desc;
                if (desc != null)
                {
                    string value0, value1 = "";
                    if (Male)
                    {
                        value0 = "He";
                        value1 = "His";
                    }
                    else
                    {
                        value0 = "She";
                        value1 = "Her";
                    }
                    WkText = desc.Replace("{0}", SItem).Replace("{1}",value0);

                }
                OnPropertyChanged("SWeeks");
            }
        }

        private string _wktext;
        public string WkText 
        {
            get => _wktext;
            set
            {
                _wktext = value;
                OnPropertyChanged("WkText");
            }
        }

        private List<string> _performance;
        public List<string> Performance 
        {
            get => new List<string> { "Good", "Okay", "Bad", "Poor" };
            set
            {
                _performance = value;
                OnPropertyChanged("Performance");
            }
        }

        private string _sperf;
        public string Sperf 
        {
            get => _sperf;
            set
            {
                _sperf=value;
                UpdatePerformaceTextBox(_sperf);
                OnPropertyChanged("Sperf");
            }
        }

        private string _perfText;
        public string PerfText
        {
            get => _perfText;
            set
            {
                _perfText = value;
                OnPropertyChanged("perfText");
            }
        }

        private ObservableCollection<string> _tasks;

        public ObservableCollection<string> Tasks 
        {
            get => new ObservableCollection<string>
            {
                "Completed","Find Extra desc"
            };
            set
            {
                _tasks = value;
                OnPropertyChanged("Tasks");
            } 
        }

        private ObservableCollection<string> _descListBox;

        public ObservableCollection<string> DescListBox
        {
            get => new ObservableCollection<string>
            {
                "Completed","Find Extra desc"
            };
            set
            {
                _descListBox = value;
                OnPropertyChanged("DescListBox");
            }
        }

        private string _extraDesc;
        public string ExtraDesc 
        { 
            get=> "He has fine knowledge of implementation but there is visible improvement from last code showcase. Note functionality api done but some of apis are not completed. He needs to work on conceptual understanding of the concepts before doing the implementation. Delay in submitting assignments. He needs to go deeper on concepts also he needs to work on presentation skill. \r\n\r\nHe has good learning ability and logical thinking. His communication is okay need to work on that. He has understanding of tasks and able to explain it. Hard working in nature. He is inquisitive in grasping new things. He is proactive and humble. He is doing good. Overall He is fine.";
            set 
            {
                _extraDesc=value;
                OnPropertyChanged("ExtraDesc");
            }
        }

        private string _stask;
        public string STasks 
        {
            get=>_stask;
            set
            {
                _stask = value;
                FinalOutPut(_stask);
                OnPropertyChanged("STasks");
            }
        }

        private string _assignmentText;
        public string AssignmentText
        {
            get=>_assignmentText;
            set
            {
                _assignmentText = value;
                OnPropertyChanged("AssignmentText");
            }
        }

        private string _finalText;
        public string FinalText
        {
            get => _finalText;
            set
            {
                _finalText = value;
                OnPropertyChanged("FinalText");
            }
        }
        private void FinalOutPut(string stask)
        {
            switch (stask)
            {
                case "Completed":
                    var finaloutput = string.Format("{0}\n\n{1}\n\n{2}\n\nAssessment Date-22-10-2022", WkText,AssignmentText ,PerfText);
                    FinalText=finaloutput;
                    break;
            }
        }

        private void UpdatePerformaceTextBox(string sperf)
        {
            string value0, value1 = "";
            if (Male)
            {
                value0 = "He";
                value1 = "His";
            }
            else
            {
                value0 = "She";
                value1 = "Her";
            }
            var desc=performance.GetValueOrDefault(sperf);
            var finalDesc=desc.Replace("{0}", value0).Replace("{1}", value1);
            PerfText=finalDesc;
        }

        private string _sItem;
        public string SItem
        {
            get => _sItem;
            set
            {
                _sItem = value;
                var _allStudents = ReadStudentDetail();
                var sex= _allStudents.Where(s => s.Name == value).FirstOrDefault().Gender;
                if (sex == 'F')
                {
                    Female = true;
                }
                else
                {
                    Male = true;
                }

                OnPropertyChanged("SItem");
            }
        }

        private bool _male;
        public bool Male 
        {
            get => _male;
            set
            {
                _male = value;
                if(Male)
                    Female=false;

                OnPropertyChanged("Male");
            }
        }
        private bool _female;
        public bool Female
        {
            get => _female;
            set
            {
                _female = value;
                if(Female)
                    Male= false;

                OnPropertyChanged("Female");
            }
        }
        #region private methods
        private List<Student> ReadStudentDetail()
        {
            using (StreamReader r = new StreamReader("C:\\Users\\Madan kumar TK\\Desktop\\TracksheetApp\\TrackSheetApplication\\TrackSheetApplication\\Student.json"))
            {
                string json = r.ReadToEnd();
                return JsonConvert.DeserializeObject<List<Student>>(json);
            }
        }

        private List<Week> ReadWeekDesc()
        {
            using (StreamReader r = new StreamReader("C:\\Users\\Madan kumar TK\\Desktop\\TracksheetApp\\TrackSheetApplication\\TrackSheetApplication\\Week.json"))
            {
                string json = r.ReadToEnd();
                return JsonConvert.DeserializeObject<List<Week>>(json);
            }
        }
        #endregion

    }


    public class Student
    {
        public string Name { get; set; }
        public char Gender { get; set; }
    }
    public class Week
    {
        public string WK { get; set; }
        public string Desc { get; set; }
    }
}
