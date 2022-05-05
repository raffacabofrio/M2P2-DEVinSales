﻿using DevInSales.Context;
using DevInSales.DTOs;
using DevInSales.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DevInSales.Controllers
{
    [Route("api/freight")]
    [ApiController]
    public class FreightController : ControllerBase
    {
        private readonly SqlContext _context;

        public FreightController(SqlContext context)
        {
            _context = context;
        }


        [HttpPost]
        [Route("state/company")]
        public async Task<ActionResult<List<StatePriceDTO>>> PostStateCompany(IEnumerable<StatePriceDTO> statePrices)
        {
            if (!ExistStateAndCompany(statePrices))
                return NotFound();

            var statePricesEnity = GetStatePrices(statePrices);
            _context.StatePrice.AddRange(statePricesEnity);

            if (await _context.SaveChangesAsync() > 0)
                return Created("", statePrices);

            return BadRequest();

        }
        private bool ExistStateAndCompany(IEnumerable<StatePriceDTO> statePrices)
        {
            var listCompany = statePrices.Select(sp => sp.ShippingCompanyId).Distinct();
            var listStates = statePrices.Select(sp => sp.StateId).Distinct();
            var companiesCount = _context.ShippingCompany.Where(sc => listCompany.Contains(sc.Id)).Count();
            var statesCount = _context.State.Where(s => listStates.Contains(s.Id)).Count();

            if (companiesCount != listCompany.Count() || statesCount != listStates.Count())
                return false;

            return true;
        }

        private IEnumerable<StatePrice> GetStatePrices(IEnumerable<StatePriceDTO> statePrices)
        {
            return statePrices.Select(cp => new StatePrice
            {
                StateId = cp.StateId,
                ShippingCompanyId = cp.ShippingCompanyId,
                BasePrice = cp.BasePrice,
            });
        }

            [HttpPost]
        [Route("city/company")]
        public async Task<ActionResult<List<CityPriceDTO>>> PostCityCompany(IEnumerable<CityPriceDTO> cityPrices)
        {
            if (!ExistCityAndCompany(cityPrices))
                return NotFound();

            var cityPricesEnity = GetCityPrices(cityPrices);
            _context.CityPrice.AddRange(cityPricesEnity);

            if (await _context.SaveChangesAsync() > 0)
                return Created("", cityPrices);

            return BadRequest();

        }

        private bool ExistCityAndCompany(IEnumerable<CityPriceDTO> cityPrices)
        {
            var listCompany = cityPrices.Select(sp => sp.ShippingCompanyId).Distinct();
            var listCities = cityPrices.Select(sp => sp.CityId).Distinct();
            var companiesCount = _context.ShippingCompany.Where(sc => listCompany.Contains(sc.Id)).Count();
            var citiesCount = _context.City.Where(s => listCities.Contains(s.Id)).Count();

            if (companiesCount != listCompany.Count() || citiesCount != listCities.Count())
                return false;

            return true;
        }

        private IEnumerable<CityPrice> GetCityPrices(IEnumerable<CityPriceDTO> cityPrices)
        {
            return cityPrices.Select(cp => new CityPrice
            {
                CityId = cp.CityId,
                ShippingCompanyId = cp.ShippingCompanyId,
                BasePrice = cp.BasePrice,
            });
        }
    }
}