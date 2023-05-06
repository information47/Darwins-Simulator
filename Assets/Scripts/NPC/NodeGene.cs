using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeGene
{
        private int id;
        public enum TYPE
        {
            Input, Output, Hidden
        }

        private TYPE type;
        private IActivation activationGene;

        public void SetActivationGene(IActivation activation)
        {
            this.ActivationGene = activation;
        }

        public NodeGene(int givenId, TYPE givenType)
        {
            Id = givenId;
            Type = givenType;

        }

        // getters and setters
        public int Id { get => id; set => id = value; }
        public TYPE Type { get => type; set => type = value; }
    public IActivation ActivationGene { get => activationGene; set => activationGene = value; }
}
