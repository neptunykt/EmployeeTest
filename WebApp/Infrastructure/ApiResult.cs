namespace WebApp.Infrastructure
{
    /// <summary>
    /// 
    /// </summary>
    public class ApiResult
    {
        public bool Result { get; set; }
        
        public string ErrorMessage { get; set; }
        

        public ApiResult() => this.Result = true;

        public ApiResult(
            string errorMessage
          )
        {
            this.Result = false;
            this.ErrorMessage = errorMessage;
          
        }
    }
}