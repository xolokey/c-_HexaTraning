using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarConnectApp.DAO
{
    public interface IReservationService<T>
    {
        T GetReservationById(int reservationId);
        List<T> GetReservationsByCustomerId(int customerId);
        T CreateReservation(T reservation);
        T UpdateReservation(int reservationId,T reservation);
        bool CancelReservation(int reservationId);
    }
}
