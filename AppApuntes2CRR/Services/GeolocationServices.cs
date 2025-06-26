using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppApuntes2CRR.Services
{
    public class GeolocationServices
    {
        private CancellationTokenSource _cancelTokenSource;
        private bool _isCheckingLocation;

        public async Task<Location> GetCurrentLocation()
        {
            try
            {
                _isCheckingLocation = true;

                GeolocationRequest request = new GeolocationRequest(GeolocationAccuracy.Medium, TimeSpan.FromSeconds(10));

                _cancelTokenSource = new CancellationTokenSource();

                Location location = await Geolocation.Default.GetLocationAsync(request, _cancelTokenSource.Token);

                if (location != null)
                {
                    return location;
                }
                //throw new Exception("Unable to get location");

            }
            // Catch one of the following exceptions:
            //   FeatureNotSupportedException
            //   FeatureNotEnabledException
            //   PermissionException
            catch (Exception ex)
            {
                // Unable to get location
                Console.WriteLine($"Error getting location: {ex.Message}");
            }
            finally
            {
                _isCheckingLocation = false;
            }
            return new Location
            {
                Latitude = 0,
                Longitude = 0,
            };
        }

        public void CancelRequest()
        {
            if (_isCheckingLocation && _cancelTokenSource != null && _cancelTokenSource.IsCancellationRequested == false)
                _cancelTokenSource.Cancel();
        }
    }
}
