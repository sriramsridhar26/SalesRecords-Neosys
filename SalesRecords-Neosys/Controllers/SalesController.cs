using Microsoft.AspNetCore.Mvc;
using SalesRecords_Neosys.DTO;
using SalesRecords_Neosys.Model;
using SalesRecords_Neosys.Repository.IRepository;

namespace SalesRecords_Neosys.Controllers
{
    [ApiController]
    [Route("/[controller]")]
    public class SalesController : ControllerBase
    {
        private readonly IUnitofWork _unitofwork;
        private readonly ILogger<SalesController> _logger;
        public SalesController(IUnitofWork unitofwork, ILogger<SalesController> logger)
        {
            _unitofwork = unitofwork;
            _logger = logger;
            _logger.LogDebug(1, "NLog injected into SalesController");
        }

        [HttpGet("{page}")]
        public async Task<IActionResult> GetAll(int page)
        {
            _logger.LogInformation(1, "GetAll method called!");
            IList<SalesRecord> salesList;
            try
            {
                salesList = await _unitofwork.SalesRecords.GetAll(page);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Issue with Database or Database Connection");
                return BadRequest("Unexpected issue, Please try later, Thank you.");
            }
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
            _logger.LogInformation(1, "GetNotEqual method called!");
            IList<SalesRecord> salesList;
            try
            {
                salesList = await _unitofwork.SalesRecords.GetNotEqual(page, val);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, $"GetNotEqual method failed during {page} and {val}");
                return BadRequest("Pagination issue. Please try with different details, Thank you.");
            }
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
            _logger.LogInformation(1, "GetByCond method called!");
            IList<SalesRecord> salesList;
            switch (c)
            {
                case '>':
                    try
                    {
                        salesList = await _unitofwork.SalesRecords.GetGreater(page, val);
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError($"GetByCond did failed due to unknown character passed as parameter. page: {page}, val: {val}, c: {c}");
                        return BadRequest("Please try later, Thank you!");
                    }
                    break;
                case '<':
                    try
                    {
                        salesList = await _unitofwork.SalesRecords.GetLesser(page, val);
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError($"GetByCond did failed due to unknown character passed as parameter. page: {page}, val: {val}, c: {c}");
                        return BadRequest("Please try later, Thank you!");
                    }
                    break;
                default:
                    _logger.LogError($"GetByCond did not execute intended methods due to unknown character passed as parameter. page: {page}, val: {val}, c: {c}");
                    return BadRequest("'<' or '>' is only allowed");
                    break;
            }
            if(salesList == null)
            {
                _logger.LogInformation($"Value was greater or lower than the column's max or min {val}");
                return BadRequest("Value exceeded the limit");
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
            if(val1 > val2)
            {
                int temp=val1;
                val1=val2;
                val2=temp;
            }
            else if(val1 == val2)
            {
                _logger.LogInformation($"Both the parameters were same val1: {val1} val2: {val2}");
                return BadRequest("Both the parameters are same, Please try with different parameters.");
            }
            try
            {
                salesList = await _unitofwork.SalesRecords.GetBetween(page, val1, val2);
            }
            catch(Exception ex)
            {
                _logger.LogError($"GetBetween failed due to an issue. page: {page}, val1: {val1}, val2: {val2}");
                return BadRequest("Please check the parameters and try again, Thank you!");
            }
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
