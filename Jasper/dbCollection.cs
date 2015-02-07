using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jasper
{
    class dbCollection
    {
    }

    [Table]
    public class userData : INotifyPropertyChanged, INotifyPropertyChanging
    {
        private int _userId;
        [Column(IsPrimaryKey = true, IsDbGenerated = true, DbType = "INT NOT NULL Identity", CanBeNull = false, AutoSync = AutoSync.OnInsert)]
        public int UserId
        {
            get
            {
                return _userId;
            }
            set
            {
                if (_userId != value)
                {
                    NotifyPropertyChanging("UserId");
                    _userId = value;
                    NotifyPropertyChanged("UserId");
                }
            }
        }

        private string _name;
        [Column]
        public string name
        {
            get
            {
                return _name;
            }
            set
            {
                if (_name != value)
                {
                    NotifyPropertyChanging("name");
                    _name = value;
                    NotifyPropertyChanged("name");
                }
            }
        }

        private string _email;
        [Column]
        public string email
        {
            get
            {
                return _email;
            }
            set
            {
                if (_email != value)
                {
                    NotifyPropertyChanging("email");
                    _email = value;
                    NotifyPropertyChanged("email");
                }
            }
        }

        [Column(IsVersion = true)]
        private Binary _version;

        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        // Used to notify the page that a data context property changed
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

    [Table]
    public class NoteText : INotifyPropertyChanged, INotifyPropertyChanging
    {
        /*
         * NoteId
         * Name
         * */

        private int _noteId;

        [Column(IsPrimaryKey = true, IsDbGenerated = true, DbType = "INT NOT NULL Identity", CanBeNull = false, AutoSync = AutoSync.OnInsert)]
        public int NoteId
        {
            get
            {
                return _noteId;
            }
            set
            {
                if (_noteId != value)
                {
                    NotifyPropertyChanging("NoteId");
                    _noteId = value;
                    NotifyPropertyChanged("NoteId");
                }
            }
        }

        private string _name;
        [Column(CanBeNull = true)]
        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                if (_name != value)
                {
                    NotifyPropertyChanging("Name");
                    _name = value;
                    NotifyPropertyChanged("Name");
                }
            }
        }

        [Column(IsVersion = true)]
        private Binary _version;

        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        // Used to notify the page that a data context property changed
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

    [Table]
    public class LocalRelation : INotifyPropertyChanging, INotifyPropertyChanged
    {
        /*
         * NoteId
         * Status
         * UserEmail
         * */

        private int _dataId;

        [Column(IsPrimaryKey = true, IsDbGenerated = true, DbType = "INT NOT NULL Identity", CanBeNull = false, AutoSync = AutoSync.OnInsert)]
        public int DataId
        {
            get
            {
                return _dataId;
            }
            set
            {
                if (_dataId != value)
                {
                    NotifyPropertyChanging("DataId");
                    _dataId = value;
                    NotifyPropertyChanged("DataId");
                }
            }
        }

        private int _noteId;
        [Column(CanBeNull = true)]
        public int NoteId
        {
            get
            {
                return _noteId;
            }
            set
            {
                if (_noteId != value)
                {
                    NotifyPropertyChanging("Name");
                    _noteId = value;
                    NotifyPropertyChanged("Name");
                }
            }
        }

        private string _userEmail;
        [Column(CanBeNull = false)]
        public string UserEmail
        {
            get
            {
                return _userEmail;
            }
            set
            {
                if (_userEmail != value)
                {
                    NotifyPropertyChanging("UserEmail");
                    _userEmail = value;
                    NotifyPropertyChanged("UserEmail");
                }
            }
        }

        private int _status;
        [Column(CanBeNull = true)]
        public int Status
        {
            get
            {
                return _status;
            }
            set
            {
                if (_status != value)
                {
                    NotifyPropertyChanging("Amount");
                    _status = value;
                    NotifyPropertyChanged("Amount");
                }
            }
        }

        // Version column aids update performance.
        [Column(IsVersion = true)]
        private Binary _version;

        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        // Used to notify the page that a data context property changed
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


    public class DbDataContext : DataContext
    {
        public static string DBConnectionString = "Data Source=isostore:/Jasper.sdf";

        // Pass the connection string to the base class.
        public DbDataContext(string connectionString)
            : base(connectionString)
        { }


        public Table<userData> userdata;
        public Table<LocalRelation> localrelation;
        public Table<NoteText> notetext;
    }
}
