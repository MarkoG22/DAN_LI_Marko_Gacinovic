using Hospital.Commands;
using Hospital.Models;
using Hospital.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Hospital.ViewModel
{
    class MainWindowViewModel : ViewModelBase
    {
        MainWindow main;

        // properties for username and password
        private string username;
        public string Username
        {
            get { return username; }
            set
            {
                username = value;
                OnPropertyChanged("Username");
            }

        }

        private string password;
        public string Password
        {
            get { return password; }
            set
            {
                password = value;
                OnPropertyChanged("Password");
            }
        }

        // properties
        private List<tblPatient> patientList;
        public List<tblPatient> PatientList
        {
            get { return patientList; }
            set { patientList = value; OnPropertyChanged("PatientList"); }
        }

        private List<tblDoctor> doctorList;
        public List<tblDoctor> DoctorList
        {
            get { return doctorList; }
            set { doctorList = value; OnPropertyChanged("DoctorList"); }
        }


        // constructor
        public MainWindowViewModel(MainWindow mainOpen)
        {
            main = mainOpen;
        }

        // command for the login button
        private ICommand logIn;
        public ICommand LogIn
        {
            get
            {
                if (logIn == null)
                {
                    logIn = new RelayCommand(param => SaveExecute(), param => CanSaveExecute());
                }
                return logIn;
            }
        }

        private bool CanSaveExecute()
        {
            return true;
        }

        /// <summary>
        /// method for checking username and password and opening the windows
        /// </summary>
        private void SaveExecute()
        {  
            if (IsPatient(username, password))
            {
                PatientView patient = new PatientView();
                patient.ShowDialog();
            }
            else if (IsDoctor(username, password))
            {
                DoctorView doctor = new DoctorView();
                doctor.ShowDialog();
            }
            else
            {
                MessageBox.Show("Wrong username or password, please try again.");
            }
        }

        // command for closing the window
        private ICommand logout;
        public ICommand Logout
        {
            get
            {
                if (logout == null)
                {
                    logout = new RelayCommand(param => CloseExecute(), param => CanCloseExecute());
                }
                return logout;
            }
        }

        /// <summary>
        /// method for closing the window
        /// </summary>
        private void CloseExecute()
        {
            try
            {
                main.Close();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Exception" + ex.Message.ToString());
            }
        }

        private bool CanCloseExecute()
        {
            return true;
        }

        /// <summary>
        /// method for checking inputs
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        private bool IsPatient(string username, string password)
        {
            try
            {
                using (HospitalEntities context = new HospitalEntities())
                {
                    tblPatient patient = (from x in context.tblPatients where x.Username == username && x.UserPassword == password select x).First();
                    return true;
                }
                
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Exception" + ex.Message.ToString());
                return false;
            }
        }

        /// <summary>
        /// method for checking inputs
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        private bool IsDoctor(string username, string password)
        {
            try
            {
                using (HospitalEntities context = new HospitalEntities())
                {
                    tblDoctor doctor = (from x in context.tblDoctors where x.Username == username && x.UserPassword == password select x).First();
                    return true;
                }

            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Exception" + ex.Message.ToString());
                return false;
            }
        }
    }
}
