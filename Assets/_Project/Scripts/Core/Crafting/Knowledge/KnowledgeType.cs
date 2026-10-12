namespace Specimen.Core.Crafting.Knowledge
{
    /// <summary>Tipos de objeto de conocimiento (GDD §17.3).</summary>
    public enum KnowledgeType
    {
        /// <summary>Común: enseña una receta.</summary>
        Scroll,
        /// <summary>Poco común: receta de equipo o de mejora de estación.</summary>
        Blueprint,
        /// <summary>Raro: variante mejorada o un grupo de 2–3 recetas.</summary>
        Treatise,
        /// <summary>Épico: un volumen de un libro de oficio (los cinco enseñan la receta maestra).</summary>
        BookVolume
    }
}
