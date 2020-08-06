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
    class PatientViewModel : ViewModelBase
    {
        PatientView patientView;

        private tblDoctor doctor;
        public tblDoctor Doctor
        {
            get { return doctor; }
            set { doctor = value; OnPropertyChanged("Doctor"); }
        }

        private List<tblDoctor> doctorList;
        public List<tblDoctor> DoctorList
        {
            get { return doctorList; }
            set { doctorList = value; OnPropertyChanged("DoctorList"); }
        }

        private tblRequest request;
        public tblRequest Request
        {
            get { return request; }
            set { request = value; OnPropertyChanged("Request"); }
        }

        private bool urgently;
        public bool Urgently
        {
            get { return urgently; }
            set { urgently = value; OnPropertyChanged("Urgently"); }
        }

        public PatientViewModel(PatientView patientViewOpen)
        {
            request = new tblRequest();
            patientView = patientViewOpen;
            DoctorList = GetAllDoctors();
        }

        private List<tblDoctor> GetAllDoctors()
        {
            try
            {
                using (HospitalEntities context = new HospitalEntities())
                {
                    List<tblDoctor> list = new List<tblDoctor>();
                    list = (from x in context.tblDoctors select x).ToList();
                    return list;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Exception" + ex.Message.ToString());
                return null;
            }
        }

        private ICommand save;
        public ICommand Save
        {
            get
            {
                if (save == null)
                {
                    save = new RelayCommand(param => SaveExecute(), param => CanSaveExecute());
                }
                return save;
            }
        }

        private bool CanSaveExecute()
        {
            return true;
        }

        private void SaveExecute()
        {
            try
            {
                using (HospitalEntities context = new HospitalEntities())
                {
                    tblRequest newRequest = new tblRequest();

                    newRequest.Reason = request.Reason;
                    newRequest.Company = request.Company;
                    newRequest.RequestDate = DateTime.Now.Date;
                    newRequest.RequestID = request.RequestID;
                    newRequest.Urgently = Urgently;

                    context.tblRequests.Add(newRequest);
                    context.SaveChanges();
                }
                patientView.Close();
            }
            catch (Exception)
            {
                MessageBox.Show("Wrong inputs, please try again.");
            }
        }

        // command for closing the window
        private ICommand close;
        public ICommand Close
        {
            get
            {
                if (close == null)
                {
                    close = new RelayCommand(param => CloseExecute(), param => CanCloseExecute());
                }
                return close;
            }
        }

        /// <summary>
        /// method for closing the window
        /// </summary>
        private void CloseExecute()
        {
            try
            {
                patientView.Close();
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
    }
}
