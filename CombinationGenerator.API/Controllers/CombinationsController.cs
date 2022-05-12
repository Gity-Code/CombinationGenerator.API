using CombinationGenerator.BL.Interfaces;
using CombinationGenerator.BL.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Buffers;
using System.ComponentModel.DataAnnotations;

namespace CombinationGenerator.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CombinationsController : ControllerBase
    {

        private readonly ICombinationRepository _combinationRepository;

        private readonly IHttpContextAccessor _httpContextAccessor;
       
        private  readonly ICommonService _commonService;

        public CombinationsController(ICombinationRepository combinationRepository,
            IHttpContextAccessor httpContextAccessor, 
            ICommonService commonService)
        {
            _combinationRepository = combinationRepository;
            _httpContextAccessor = httpContextAccessor;
            _commonService = commonService;
        }

        [HttpGet]
        [Route("start/{n}")]
        public  IActionResult GetStart(int n)
        {
            try
            {
                if(_commonService.GetNumberCombinations() != n)
                    _commonService.SetNumberCombinations(n);
                return Ok( _combinationRepository.GetPossibleCombinationsNumber(n));
            }
            catch(Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet]
        [Route("{n}/{pageNumber}/{pageSize}")]
        public async Task<IActionResult> GetCombination([FromRoute] int n , 
            [FromRoute][Required] int pageNumber, [FromRoute][Required] int pageSize)
        {
            try
            {
                var combinations = await _combinationRepository.GetCombination(n, pageNumber, pageSize);
              
                return Ok(combinations);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

     

        [HttpGet]
        [Route("getNext")]
        public async Task<IActionResult> GetNextAPI()
        {
            try
            {
                List<int> array = new List<int>();
                List<int> prevValues = _commonService.GetLastPermutationValues().ToList();
                if (prevValues.Count==0)
                {
                    int n = _commonService.GetNumberCombinations();
                    prevValues = Enumerable.Range(1, n).ToList();
                }
                var result = _combinationRepository.NextPermutation(prevValues.ToArray());
                _commonService.SetLastPermutationValues(result);
                return Ok(result);
            }
            catch(Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

    }
}
