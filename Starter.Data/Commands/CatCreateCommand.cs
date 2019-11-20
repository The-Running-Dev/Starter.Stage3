using Starter.Framework.Clients;

namespace Starter.Data.Commands
{
    public class CatCreateCommand : CatCommand
    {
        public CatCreateCommand(IApiClient apiClient) : base(apiClient)
        {
        }

        //public override bool CanExecute(object parameter)
        //{
        //    return true;
        //}

        //public override void Execute(object parameter)
        //{
        //    ApiClient.CreateAsync(parameter);
        //}
    }
}