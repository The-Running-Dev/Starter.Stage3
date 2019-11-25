namespace Starter.Data.Entities
{
    public class Message<T>
    {
        public MessageCommand Command { get; set; }

        public T Entity { get; set; }

        public string Type { get; set; }

        public Message(MessageCommand command, T entity)
        {
            Command = command;
            Entity = entity;
            Type = nameof(T);
        }
    }
}