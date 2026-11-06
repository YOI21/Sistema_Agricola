using System.Reflection;
using Sistema_Agricola.BLL;
using Sistema_Agricola.Entities;

namespace Sistema_Agricola.Tests;

public sealed class DependenciasArquitecturaTests
{
    [Fact]
    public void BllYEntidades_NoReferencianSqlClientAntiguo()
    {
        Assert.DoesNotContain(
            typeof(BLL_Terreno).Assembly.GetReferencedAssemblies(),
            referencia => referencia.Name == "System.Data.SqlClient");
        Assert.DoesNotContain(
            typeof(Entitie_Terreno).Assembly.GetReferencedAssemblies(),
            referencia => referencia.Name == "System.Data.SqlClient");
    }

    [Fact]
    public void ApiPublicaBll_NoExponeTiposDeReglasAgricolas()
    {
        Assembly ensamblado = typeof(BLL_Terreno).Assembly;
        List<string> filtraciones = new();

        foreach (Type tipo in ensamblado.GetExportedTypes())
        {
            foreach (MethodInfo metodo in tipo.GetMethods(
                         BindingFlags.Public |
                         BindingFlags.Instance |
                         BindingFlags.Static |
                         BindingFlags.DeclaredOnly))
            {
                RegistrarSiFiltra(
                    metodo.ReturnType,
                    $"{tipo.Name}.{metodo.Name}: retorno",
                    filtraciones);
                foreach (ParameterInfo parametro in metodo.GetParameters())
                {
                    RegistrarSiFiltra(
                        parametro.ParameterType,
                        $"{tipo.Name}.{metodo.Name}: {parametro.Name}",
                        filtraciones);
                }
            }

            foreach (PropertyInfo propiedad in tipo.GetProperties(
                         BindingFlags.Public |
                         BindingFlags.Instance |
                         BindingFlags.Static |
                         BindingFlags.DeclaredOnly))
            {
                RegistrarSiFiltra(
                    propiedad.PropertyType,
                    $"{tipo.Name}.{propiedad.Name}",
                    filtraciones);
            }
        }

        Assert.Empty(filtraciones);
    }

    [Fact]
    public void RecomendacionAgricola_SeConsumeDesdeBll()
    {
        EvaluacionAgricola evaluacion = BLL_RecomendacionesAgricolas.Evaluar(
            "Arroz",
            new CondicionesAgroclimaticas
            {
                FechaSiembra = new DateTime(2026, 11, 1),
                Provincia = "Duarte",
                TipoSuelo = "Franco",
                PH = 6m,
                Humedad = 65m,
                Drenaje = "Bueno",
                Pendiente = 3m,
                PrecipitacionAnual = 1500m,
                DisponibilidadRiego = true,
                Variedad = "Referencia"
            });

        Assert.NotEqual(NivelRecomendacionAgricola.SinInformacion, evaluacion.Nivel);
        Assert.NotEmpty(evaluacion.Criterios);
        Assert.NotEmpty(evaluacion.Fuentes);
        Assert.Contains("validación agronómica", evaluacion.EstadoValidacion);
    }

    private static void RegistrarSiFiltra(
        Type tipo,
        string miembro,
        ICollection<string> filtraciones)
    {
        if (EsTipoDeReglasAgricolas(tipo))
        {
            filtraciones.Add(miembro + " -> " + tipo.FullName);
        }
    }

    private static bool EsTipoDeReglasAgricolas(Type tipo)
    {
        if (tipo.IsByRef || tipo.IsPointer || tipo.IsArray)
        {
            return EsTipoDeReglasAgricolas(tipo.GetElementType()!);
        }

        if (string.Equals(
                tipo.Namespace,
                "Sistema_Agricola.Pr",
                StringComparison.Ordinal))
        {
            return true;
        }

        return tipo.IsGenericType &&
               tipo.GetGenericArguments().Any(EsTipoDeReglasAgricolas);
    }
}
