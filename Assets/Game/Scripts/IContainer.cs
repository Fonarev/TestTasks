using UnityEngine;

namespace Assets.Game.Scripts
{
    public interface IContainer 
    {
       Transform Parent { get; }
    }
}