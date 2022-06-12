using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SalesRecords_Neosys.DTO;
using SalesRecords_Neosys.Model;
using SalesRecords_Neosys.Repository.IRepository;

namespace SalesRecords_Neosys.Controllers
{
    [ApiController]
    [Route("/[controller]")]
    public class SalesController : Controller
    {
        private readonly IUnitofWork _unitofwork;
        private readonly IMapper _mapper;
        public SalesController(IUnitofWork unitofwork, IMapper mapper)
        {
            _unitofwork = unitofwork;
            _mapper = mapper;
        }

        [HttpGet("{page}")]
        public async Task<IActionResult> GetAll(int page)
        {
            IList<SalesRecord> salesList;
            salesList = await _unitofwork.SalesRecords.GetAll(page);
            var response = new SalesResponseDTO
            {
                salesRecords = (List<SalesRecord>)salesList,
                CurrentPage = page,
                Pages = _unitofwork.SalesRecords.totalPages('\0',0,0)
            };
            return Ok(response);
        }
        [HttpGet("totalprofit/GetNE/{val}/{page}")]
        public async Task<IActionResult> GetNotEqual(int page, int val)
        {
            IList<SalesRecord> salesList;
            salesList = await _unitofwork.SalesRecords.GetNotEqual(page, val);
            var response = new SalesResponseDTO
            {
                salesRecords = (List<SalesRecord>)salesList,
                CurrentPage = page,
                Pages = _unitofwork.SalesRecords.totalPages('n', val, 0)
            };
            return Ok(response);
        }
        [HttpGet("totalprofit/{c}/{val}/{page}")]
        public async Task<IActionResult> GetByCond(int page, char c, int val)
        {

            IList<SalesRecord> salesList;
            switch (c)
            {
                case '>':
                    salesList = await _unitofwork.SalesRecords.GetGreater(page, val);
                    break;
                case '<':
                    salesList = await _unitofwork.SalesRecords.GetLesser(page, val);
                    break;
                default:
                    return BadRequest();
                    break;
            }

            var response = new SalesResponseDTO
            {
                salesRecords = (List<SalesRecord>)salesList,
                CurrentPage = page,
                Pages = _unitofwork.SalesRecords.totalPages(c, val,0)
            };
            return Ok(response);
        }
        [HttpGet("totalprofit/between/{val1}/{val2}/{page}")]
        public async Task<IActionResult> GetBetween(int page, int val1, int val2)
        {
            IList<SalesRecord> salesList;
            salesList = await _unitofwork.SalesRecords.GetBetween(page, val1, val2);
            var response = new SalesResponseDTO
            {
                salesRecords = (List<SalesRecord>)salesList,
                CurrentPage = page,
                Pages = _unitofwork.SalesRecords.totalPages('b',val1, val2)
            };
            return Ok(response);
        }
    }
}
