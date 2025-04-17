using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarConnectApp.DAO
{
    public interface IVehicleService<T>
    {
        T GetVehicleById(int vehicleId);
        List<T> GetAvailableVehicles();
        T AddVehicle(T vehicle);
        T UpdateVehicle(int vehicleID);
        bool RemoveVehicle(int vehicleID);

    }
}
