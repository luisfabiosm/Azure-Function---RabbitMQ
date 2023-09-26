using funcGravarLog.Domain.Core.Enums;
using System;

namespace Domain.Core.Model
{
    public record Evento
    {
        public string ID { get; set; }
        public string? ReqID { get; set; }

        public DateTime DataTransacao { get; set; }
        public string SistemaOrigem { get; set; }
        public EnumStatusTransacao Status { get; set; }
        public object DadosTransacao { get; set; }
        public DateTime DataAtualizacao { get; set; }
    }
}
