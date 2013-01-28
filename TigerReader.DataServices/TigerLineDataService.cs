using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using AutoMapper;
using TigerReader.Data;
using TigerReader.Domain;

namespace TigerReader.DataServices
{
    public class TigerLineDataService : IDisposable
    {
        private bool _disposed;
        private Streets_SanDiegoEntities _entities;

        public TigerLineDataService()
        {
            _entities = new Streets_SanDiegoEntities();
            CreateMaps();
        }

        public TigerLineDataService(string connectionString)
        {
            _entities = new Streets_SanDiegoEntities(connectionString);
            CreateMaps();
        }


        private void CreateMaps()
        {
            Mapper.CreateMap<GetAllStreetNames_Result, StreetName>()
                .ForMember(dest => dest.RecordId, opt => opt.MapFrom(src => src.Id));

            Mapper.CreateMap<GetStreetByAddress_Result, StreetSummary>();

            Mapper.CreateMap<GetStreetNameById_Result, StreetName>()
                .ForMember(dest => dest.RecordId, opt => opt.MapFrom(src => src.Id));

            Mapper.CreateMap<GetStreetNamesByIdList_Result, StreetName>()
                .ForMember(dest => dest.RecordId, opt => opt.MapFrom(src => src.Id));

            Mapper.CreateMap<GetPlaceByCityAndState_Result, Place>()
                .ForMember(dest => dest.RecordId, opt => opt.MapFrom(src => src.Id));

            Mapper.CreateMap<GetStreetSegmentsByTLIDOrdered_Result, StreetSegment>()
                .ForMember(dest => dest.RecordId, opt => opt.MapFrom(src => src.Id));
        }

        public IList<StreetSegment> GetStreetSegmentsByTigerLineIdOrdered(int tlid)
        {
            return Mapper.Map<IList<StreetSegment>>(_entities.GetStreetSegmentsByTLIDOrdered(tlid).ToList());
        }
        public IList<StreetName> GetAllStreetNames()
        {
            return Mapper.Map<IList<StreetName>>(_entities.GetAllStreetNames().ToList());
        }

        public Place GetPlaceByCityAndState(string city, string state)
        {
            return Mapper.Map<Place>(_entities.GetPlaceByCityAndState(city, state).FirstOrDefault());
        }

        public IList<Place> GetPlacesByState(string state)
        {
            return Mapper.Map<IList<Place>>(_entities.GetPlacesByState(state).ToList());
        }

        public void CreateStreetSegment(int? tlId, int sequence, double latitude, double longitude)
        {
            _entities.CreateStreetSegment(tlId, sequence, latitude, longitude);
        }

        public void CreateStreet(int? tlid, string censusFeatureClassCode, string directionPrefix, string name,
                                 string type, string places, string directionSuffix)
        {
            _entities.CreateStreet(tlid, censusFeatureClassCode, directionPrefix, name, type, places,
                                   directionSuffix);
        }

        public void CreateAddress(int? tigerLineId, int? rangeId, string first, string last)
        {
            _entities.CreateAddress(tigerLineId, rangeId, first, last);
        }

        public StreetSummary GetStreetSummaryByAddress(int? number, int? placeId, string street)
        {
            return Mapper.Map<StreetSummary>(_entities.GetStreetByAddress(number, placeId, street).FirstOrDefault());
        }

        public IList<StreetName> GetStreetNamesByIdList(IList<int> idList)
        {
            return
                Mapper.Map<IList<StreetName>>(
                    _entities.GetStreetNamesByIdList(string.Join(",",
                                                                 idList.Select(
                                                                     i => i.ToString(CultureInfo.InvariantCulture)).
                                                                     ToList())));

        }

        public StreetName GetStreetNameById(int? id)
        {
            return Mapper.Map<StreetName>(_entities.GetStreetNameById(id).FirstOrDefault());
        }

        public void CreatePlace(int? placeId, string stateFips, string countyFips, string placeFips, string stateName,
                                string countyName, string placeName)
        {
            _entities.CreatePlace(placeId, stateFips, countyFips, placeFips, stateName, countyName, placeName);
        }

        public void Dispose()
        {
            if (_disposed || _entities == null) return;

            _disposed = true;
            _entities.Dispose();

            _entities = null;
        }
    }
}
