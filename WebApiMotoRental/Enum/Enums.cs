﻿namespace WebApiMotoRental.Enum
{
    public enum eTipoUsuario
    {
        tuNenhum,
        tuAdmin,
        tuEntregador
    }

    public enum eTipoPessoaDocumento
    {
        tpdNenhum,
        tpdRG,
        tpdCNPJ,
        tpdCPF,
        tpdCNH
    }

    public enum eTipoCNH
    {
        tcnhNenhum,
        tcnhACC,
        tcnhA,
        tcnhB,
        tcnhC,
        tcnhD
    }

     /*   ACC – Autorização para Conduzir Ciclomotor. ...
    Categoria A – Motocicletas, motonetas e triciclos. ...
    Categoria B – Carros, picapes e vans. ...
    Categoria C – Caminhões, caminhonetes e vans de Carga. ...
    Categoria D – Ônibus, micro-ônibus, vans de passageiros.*/

    [Flags]
    public enum ValidacaoPessoaResultado
    {
        Default = 0,
        NumeroDocumentoCadastrado = 1,
        TipoDocumentoNaoInformado = 2,
        NomePessoaInvalido = 4,
        Ok = 8
    }

    [Flags]
    public enum ValidacaoLocacaoResultado
    {
        Default = 0,
        DataInicioNaoInformada = 1,
        DataTerminoNaoInformada = 2,
        DataPrevisaoTerminoNaoInformada = 4,
        InicioLocacaoPrimeiroDiaAposInclusao = 8,
        EntregadorNaohabilitadoCategoriaA = 16,
        NumeroDiasForadoPlanoDisponivel = 32,
        Ok = 64
    }

    public enum LocacaoCalculoTipo
    {
        Default,
        PeriodoExatoTermino,
        PeriodoInferiorTermino,
        PeriodoSuperiorTermino
    }

}
