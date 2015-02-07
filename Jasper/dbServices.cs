using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jasper
{
    class dbServices : INotifyPropertyChanged
    {
        private DbDataContext userDB;

        private ObservableCollection<userData> _userData;
        private ObservableCollection<LocalRelation> _localRelation;
        private ObservableCollection<NoteText> _noteText;
        public Jasper.PopUpUserControl.onCreateNote oncreate = new PopUpUserControl.onCreateNote();

        public ObservableCollection<userData> userdata
        {
            get
            {
                return _userData;
            }
            set
            {
                if (_userData != value)
                {
                    NotifyPropertyChanging("userdata");
                    _userData = value;
                    NotifyPropertyChanged("userdata");
                }
            }
        }

        public ObservableCollection<LocalRelation> localrelation
        {
            get
            {
                return _localRelation;
            }
            set
            {
                if (_localRelation != value)
                {
                    NotifyPropertyChanging("localrelation");
                    _localRelation = value;
                    NotifyPropertyChanged("localrelation");
                }
            }
        }

        public ObservableCollection<NoteText> notetext
        {
            get
            {
                return _noteText;
            }
            set
            {
                if (_noteText != value)
                {
                    NotifyPropertyChanging("notetext");
                    _noteText = value;
                    NotifyPropertyChanged("notetext");
                }
            }
        }

        public dbServices()
        {
            userDB = new DbDataContext(DbDataContext.DBConnectionString);
        }

        public string returnEmailofUser()
        {
            userDB = new DbDataContext(DbDataContext.DBConnectionString);
            var userInDB = from userData _user in userDB.userdata select _user;
            userdata = new ObservableCollection<userData>(userInDB);
            return userdata[0].email;
        }

        public void addnoteInDb(Jasper.PopUpUserControl.onCreateNote res, string name)
        {
            userDB = new DbDataContext(DbDataContext.DBConnectionString);
            var userInDB = from NoteText _user in userDB.notetext select _user;
            notetext = new ObservableCollection<NoteText>(userInDB);

            var dbData = notetext.ToList();

            string _t = JsonConvert.SerializeObject(dbData);
            System.Diagnostics.Debug.WriteLine(_t);

            NoteText newUser = new NoteText
            {
                NoteId = res.Id,
                Name = name
            };

            try
            {
                NotifyPropertyChanging("notetext");
                notetext.Add(newUser);
                userDB.notetext.InsertOnSubmit(newUser);
                userDB.SubmitChanges();
                NotifyPropertyChanged("notetext");
                System.Diagnostics.Debug.WriteLine("Added note in notetext database");
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e.ToString());
            }
        }

        public bool isUserLoggedIn()
        {
            var userInDB = from userData _user in userDB.userdata select _user;
            userdata = new ObservableCollection<userData>(userInDB);

            if (userdata[0].email != "")
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        // Used to notify the app that a property has changed.
        private void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        #endregion

        #region INotifyPropertyChanging Members

        public event PropertyChangingEventHandler PropertyChanging;

        // Used to notify the data context that a data context property is about to change
        private void NotifyPropertyChanging(string propertyName)
        {
            if (PropertyChanging != null)
            {
                PropertyChanging(this, new PropertyChangingEventArgs(propertyName));
            }
        }

        #endregion
    }
}
