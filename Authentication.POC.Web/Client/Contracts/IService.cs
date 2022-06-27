namespace Authentication.POC.Web.Client.Contracts
{
    public interface IService<TItemDTO, TAddDTO, TEditDTO>
    {
        Task<IEnumerable<TItemDTO>> GetItems();
        Task<TItemDTO> GetItem(Guid id);
        Task AddItem(TAddDTO dto);
        Task EditItem(TEditDTO dto);
        Task DeleteItem(Guid id);
    }
}
