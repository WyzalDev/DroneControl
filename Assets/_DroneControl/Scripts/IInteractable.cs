namespace _DroneControl.Scripts
{
    public interface IInteractable
    {
        public string TooltipMessage { get; set; }

        public void Interact();
    }
}