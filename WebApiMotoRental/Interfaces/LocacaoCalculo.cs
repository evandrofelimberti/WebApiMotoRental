using WebApiMotoRental.Enum;

namespace WebApiMotoRental.Interfaces
{
    public interface LocacaoCalculoImpl
    {
        int CalcularDiasLocado();
        double CalcularValor();
    }

    public class LocacaoCalculoFactory
    {
        public LocacaoCalculoImpl CreateLocacaoCalculo(LocacaoCalculoTipo tipo)
        {
            switch (tipo)
            {
                case LocacaoCalculoTipo.PeriodoExatoTermino:
                    return new LocacalCalculoPeriodoExatoTermino();
                case LocacaoCalculoTipo.PeriodoInferiorTermino:
                    return new LocacaoCalculoPeriodoInferiorTermino();
                case LocacaoCalculoTipo.PeriodoSuperiorTermino:
                    return new LocacaoCalculoPeriodoSuperiorTermino();
                default: throw new ArgumentException("Tipo locacao calculo inválido!");

            }          
        }
    }

    public class LocacalCalculoPeriodoExatoTermino : LocacaoCalculoImpl
    {
        public int CalcularDiasLocado()
        {
            throw new NotImplementedException();
        }

        public double CalcularValor()
        {
            throw new NotImplementedException();
        }
    }

    public class LocacaoCalculoPeriodoInferiorTermino : LocacaoCalculoImpl
    {
        public int CalcularDiasLocado()
        {
            throw new NotImplementedException();
        }

        public double CalcularValor()
        {
            throw new NotImplementedException();
        }
    }

    public class LocacaoCalculoPeriodoSuperiorTermino : LocacaoCalculoImpl
    {
        public int CalcularDiasLocado()
        {
            throw new NotImplementedException();
        }

        public double CalcularValor()
        {
            throw new NotImplementedException();
        }
    }
}
