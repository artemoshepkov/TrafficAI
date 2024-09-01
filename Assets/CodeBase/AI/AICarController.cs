using System;
using System.Collections.Generic;
using CodeBase.AI.Base;
using CodeBase.AI.States;
using CodeBase.Car;
using CodeBase.Infrastructure;
using CodeBase.Roads;
using Unity.VisualScripting;
using UnityEngine;
using Sequence = CodeBase.AI.Base.Sequence;

namespace CodeBase.AI
{
    public class AICarController : TreeBehaviour
    {
        private TreeBehaviour _treeBeh;

        [Header("References")]
        [SerializeField] private CarMover _carMover;
        [SerializeField] private Transform _transform;
        [SerializeField] private RoadNode _currentNode;


        [Header("Settings")]
        [SerializeField] private bool _isStop;

        protected override Node SetupTree()
        {
            return new Selector
            (
                new List<Node>()
                {
                    new Sequence
                    (
                        new List<Node>()
                        {
                            new Leaf(new Condition(() => _isStop)),
                            new Leaf(new ActionStrategy(() => _carMover.SetBrake(1f)))
                        },
                        "Verify To Stop"
                    ),
                    new Sequence
                    (
                        new List<Node>()
                        {
                            new Leaf(new Condition(() => !_isStop)),
                            new Leaf(new ActionStrategy(() => _carMover.SetBrake(0f))),
                            new Leaf(new OnGraphRandomDriving(_carMover, _currentNode, _transform))
                        },
                        "Verify To Drive"
                    ),
                }
            );
        }

        public void Init(RoadNode roadNode) => _currentNode = roadNode;
    }
}