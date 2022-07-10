namespace Core.GameSetup
{
    using System;
    using System.ComponentModel;

    public class SteamLibraryPath : ICloneable, INotifyPropertyChanged
    {
        public SteamLibraryPath()
        {
            // MyBase.New()

            this.theMacro = "<library1>";
            this.thePath = @"C:\Program Files (x86)\Steam";
            this.theUseCount = 0;
        }

        protected SteamLibraryPath(SteamLibraryPath originalObject)
        {
            this.theMacro = originalObject.Macro;
            this.thePath = originalObject.LibraryPath;
            this.theUseCount = originalObject.UseCount;
        }

        public object Clone()
        {
            return new SteamLibraryPath(this);
        }



        public string Macro
        {
            get
            {
                return this.theMacro;
            }
            set
            {
                if (this.theMacro != value)
                {
                    this.theMacro = value;
                    NotifyPropertyChanged("Macro");
                }
            }
        }

        public string LibraryPath
        {
            get
            {
                return this.thePath;
            }
            set
            {
                this.thePath = value;
                NotifyPropertyChanged("LibraryPath");
            }
        }

        public int UseCount
        {
            get
            {
                return this.theUseCount;
            }
            set
            {
                this.theUseCount = value;
                NotifyPropertyChanged("UseCount");
            }
        }





        public event PropertyChangedEventHandler PropertyChanged;



        protected void NotifyPropertyChanged(string info)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(info));
        }



        private string theMacro;
        private string thePath;
        private int theUseCount;
    }

}
