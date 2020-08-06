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
    class DoctorViewModel : ViewModelBase
    {
        DoctorView doctorView;

        // properties
        private List<tblRequest> requestList;
        public List<tblRequest> RequestList
        {
            get { return requestList; }
            set { requestList = value; OnPropertyChanged("RequestList"); }
        }

        private tblRequest request;
        public tblRequest Request
        {
            get { return request; }
            set { request = value; OnPropertyChanged("Request"); }
        }

        // constructor
        public DoctorViewModel(DoctorView doctorViewOpen)
        {
            doctorView = doctorViewOpen;
            RequestList = GetAllRequests();
        }

        // command for deleting the request
        private ICommand deleteRequest;
        public ICommand DeleteRequest
        {
            get
            {
                if (deleteRequest == null)
                {
                    deleteRequest = new RelayCommand(param => DeleteRequestExecute(), param => CanDeleteRequestExecute());
                }
                return deleteRequest;
            }
        }

        private void DeleteRequestExecute()
        {
            try
            {
                using (HospitalEntities context = new HospitalEntities())
                {
                    int id = request.RequestID;

                    MessageBoxResult messageBoxResult = MessageBox.Show("Are you sure you want to delete request?", "Delete Confirmation", MessageBoxButton.YesNo);

                    if (messageBoxResult == MessageBoxResult.Yes)
                    {
                        tblRequest requestToDelete = (from x in context.tblRequests where x.RequestID == id select x).First();
                        context.tblRequests.Remove(requestToDelete);
                        context.SaveChanges();

                        RequestList = GetAllRequests();
                    }                    
                }
            }
            catch (Exception)
            {
                MessageBox.Show("The request can not be deleted, please try again.");
            }
        }

        private bool CanDeleteRequestExecute()
        {
            return true;
        }

        /// <summary>
        /// method for getting all requests to the list
        /// </summary>
        /// <returns></returns>
        private List<tblRequest> GetAllRequests()
        {
            try
            {
                using (HospitalEntities context = new HospitalEntities())
                {
                    List<tblRequest> list = new List<tblRequest>();
                    list = (from x in context.tblRequests select x).ToList();
                    return list;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Exception" + ex.Message.ToString());
                return null;
            }
        }
    }
}
