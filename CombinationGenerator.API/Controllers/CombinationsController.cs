using CombinationGenerator.BL.Interfaces;
using CombinationGenerator.BL.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Buffers;

namespace CombinationGenerator.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CombinationsController : ControllerBase
    {

        private readonly ICombinationRepository _combinationRepository;

        private readonly IHttpContextAccessor _httpContextAccessor;
        int count; 


        public CombinationsController(ICombinationRepository combinationRepository,
            IHttpContextAccessor httpContextAccessor)
        {
            _combinationRepository = combinationRepository;
            _httpContextAccessor = httpContextAccessor;
        }

        [HttpGet]
        [Route("start/{n}")]
        public async Task<IActionResult> GetStart(int n)
        {
            try
            {

                return Ok(await _combinationRepository.GetPossibleCombinationsNumber(n));
            }
            catch(Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet]
        [Route("{n}/{pageNumber}/{pageSize}")]
        public async Task<IActionResult> GetCombination([FromRoute] int n , 
            [FromRoute] int pageNumber, [FromRoute] int pageSize)
        {
            try
            {
                //array = new int[n];
                return Ok(await _combinationRepository.GetCombination(n, pageNumber, pageSize));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        //private static ArrayPool<byte> _arrayPool = ArrayPool<byte>.Create();
        private static List<int> array;

        [HttpGet]
        [Route("getNext")]
        public async Task<IActionResult> GetNextAPI()
        {
            try
            {
                int n = 3;
                IEnumerable<int> enumerable = Enumerable.Range(1, n);

                array = new List<int>();
                //var username = _httpContextAccessor.HttpContext.User.Identity.Name;

                //static int[]
                //IEnumerable<int> list = Enumerable.Range(1, n);
                //List<T> asList = list.ToList();
                //T[] array=new T[]
                //int[] arr = {1, 2, 3};
                //List<int> list = new List<int>();
                //var x = 'c';
                // first time
                if (array.Any(x => x.Equals(0)))
                {
                  
                   //array = enumerable;
                    return Ok(enumerable);
                }
                //array = CombinationRepository.NextPermutation(array);
                //HttpContext.Response.RegisterForDispose(_arrayPool);
                return Ok(array);
            }
            catch(Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

    }
}
