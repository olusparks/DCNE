using DCNE_Cake.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace DCNE_Cake.Repository
{
    public interface ILocationRepository
    {
        IEnumerable<Location> GetLocation();
        Location GetLocationById(int locationId);
        void InsertLocation(Location location);
        void UpdateLocation(Location location);
        void DeleteLocation(int locationId);
        //void Save();
    }

    public class LocationRepository : ILocationRepository
    {
        private AppContext context;

        public LocationRepository(AppContext context)
        {
            this.context = context;
        }

        public IEnumerable<Location> GetLocation()
        {
            return context.Locations.ToList();
        }

        public Location GetLocationById(int locationId)
        {
            var result = context.Locations.Find(locationId);
            return result;
        }

        public void InsertLocation(Location location)
        {
            context.Locations.Add(location);
        }

        public void UpdateLocation(Location location)
        {
            context.Entry(location).State = EntityState.Modified;
        }

        public void DeleteLocation(int locationId)
        {
            Location location = GetLocationById(locationId);
            context.Locations.Remove(location);
        }
    }
}