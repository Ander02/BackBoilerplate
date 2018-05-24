using Business.Util.Comparators;
using Newtonsoft.Json;
using System.Linq;

namespace Business.Util.Extensions
{
    public static class ObjectExtensions
    {
        public static bool NotEquals(this object obj1, object obj2) => !obj1.Equals(obj2);

        public static string ToJson(this object obj) => JsonConvert.SerializeObject(obj);

        public static string ToJson(this object obj, JsonSerializerSettings settings) => JsonConvert.SerializeObject(obj, settings);
        
        public static void UpdatePropsByReflection(this object update, object data)
        {
            //Pegar todos os atributos de data
            var dataProps = data.GetType().GetProperties().OrderBy(p => p.Name).ToArray();

            //Pegar todos os atributos em update que tem em data
            var targetProps = update.GetType().GetProperties().Intersect(dataProps, new EqualityComparerPropertyInfoName()).OrderBy(p => p.Name).ToArray();

            //Para cada atributo
            for (int i = 0; i < dataProps.Length; i++)
            {
                var targetProp = targetProps[i];
                var dataProp = dataProps[i];

                //Se ambos possuem o mesmo nome e tipo
                if (targetProp.Name.Equals(dataProp.Name) && targetProp.GetType().Name.Equals(dataProp.GetType().Name))
                {
                    var dataValue = dataProp.GetValue(data);
                    //Se o valor não é nulo, atualiza
                    if (dataValue != null) targetProp.SetMethod.Invoke(update, new object[] { dataValue });
                }
            }
        }
    }
}
