namespace Authentication.POC.Web.Client.Pages
{
    public partial class Roles
    {
        private record Role(Guid Id, string Name, DateTime CreatedAt, DateTime? UpdatedAt);

        protected override Task OnInitializedAsync()
        {
            
        }

        private List<Role>? Items { get; set; }
    }
}
