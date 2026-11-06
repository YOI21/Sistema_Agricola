# Fuentes y validación de las reglas agrícolas

## Alcance

El recomendador es una herramienta de orientación y descarte inicial. No es un
diagnóstico agronómico, no sustituye el análisis de suelo ni decide por sí solo
si una siembra debe ejecutarse.

Los umbrales implementados en `Sistema_Agricola.Pr/CultivoData.cs` son una
síntesis preliminar de bibliografía institucional. No deben describirse en la
tesis como valores experimentalmente validados por este proyecto hasta que un
profesional de agronomía complete y firme la matriz de validación.

## Bibliografía institucional

| Código | Institución | Uso en el sistema | Enlace |
|---|---|---|---|
| FAO-ECOCROP | FAO | pH, precipitación, textura, drenaje y requisitos ecológicos por especie | https://www.fao.org/geospatial/data-and-tools/data-portals/ecocrop/ |
| MARD-CALENDARIO | Ministerio de Agricultura de la República Dominicana | meses de siembra, cosecha y ciclo de cultivos nacionales | https://agricultura.gob.do/wp-content/uploads/2022/09/Indice-Estacional-de-Precios-2019.pdf |
| INDOMET-PRECIPITACION | Instituto Dominicano de Meteorología | contraste de lluvia por estación, provincia y período | https://indomet.gob.do/transparencia/datos-abiertos/acumulados-de-precipitacion/ |
| CEDAF-GUIAS | CEDAF | prácticas y requisitos técnicos adaptados a República Dominicana | https://www.cedaf.org.do/guias-tecnicas/ |
| DIGERA-GUIAS | DIGERA | riesgos y condiciones de referencia por cultivo | https://digera.gob.do/seguro-agropecuario/guia-de-cultivos/ |

Consulta documental realizada el 1 de noviembre de 2026.

## Criterios y procedencia

| Criterio | Fuente principal | Verificación requerida |
|---|---|---|
| pH, lluvia, suelo y drenaje | FAO-ECOCROP y guías CEDAF | Comparar cada especie con su ficha y la guía local correspondiente. |
| Meses de siembra | MARD-CALENDARIO | Confirmar variaciones por zona, riego y variedad. |
| Zona y precipitación | INDOMET-PRECIPITACION | Sustituir promedios generales por datos de la estación más cercana. |
| Pendiente y humedad | CEDAF-GUIAS y DIGERA-GUIAS | Validar umbral por cultivo y sistema de manejo. |
| Variedad y riego | ficha del material y técnico local | Registrar variedad, proveedor y recomendación técnica. |

## Matriz de validación agronómica

Esta tabla debe completarse con un ingeniero agrónomo antes de presentar las
reglas como validadas.

| Cultivo o grupo | pH | lluvia | suelo y drenaje | pendiente | meses | decisión | observación | profesional y exequátur | fecha y firma |
|---|---:|---:|---|---:|---|---|---|---|---|
| Arroz | Pendiente | Pendiente | Pendiente | Pendiente | Pendiente | Pendiente | | | |
| Café | Pendiente | Pendiente | Pendiente | Pendiente | Pendiente | Pendiente | | | |
| Cacao | Pendiente | Pendiente | Pendiente | Pendiente | Pendiente | Pendiente | | | |
| Plátano y guineo | Pendiente | Pendiente | Pendiente | Pendiente | Pendiente | Pendiente | | | |
| Frutales | Pendiente | Pendiente | Pendiente | Pendiente | Pendiente | Pendiente | | | |
| Cereales y leguminosas | Pendiente | Pendiente | Pendiente | Pendiente | Pendiente | Pendiente | | | |
| Raíces y tubérculos | Pendiente | Pendiente | Pendiente | Pendiente | Pendiente | Pendiente | | | |
| Hortalizas | Pendiente | Pendiente | Pendiente | Pendiente | Pendiente | Pendiente | | | |

## Evidencia para la tesis

Conservar junto al proyecto las fichas consultadas, la versión de esta matriz,
el nombre del profesional que la revisó y las correcciones realizadas. En la
metodología debe explicarse que el sistema compara mediciones registradas con
rangos de referencia y devuelve una orientación trazable, no una predicción de
inteligencia artificial.
