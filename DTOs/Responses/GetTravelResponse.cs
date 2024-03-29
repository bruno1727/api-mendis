﻿namespace ApiMendis.DTOs.Responses
{
    public class GetTravelResponse
    {
        public IEnumerable<Destino> Destinos { get; init; }

        public GetTravelResponse(IEnumerable<Destino> destinos) 
        {
            Destinos = destinos;
        }

        public class Destino
        {
            public string Cidade { get; init; }
            public string Regiao { get; init; }
            public string Caracteristicas { get; init; }

            public Destino(string cidade, string regiao, string caracteristicas)
            {
                Cidade = cidade;
                Regiao = regiao;
                Caracteristicas = caracteristicas;
            }
        }
    }
}
