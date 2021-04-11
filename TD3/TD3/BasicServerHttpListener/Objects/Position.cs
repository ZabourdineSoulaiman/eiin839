using System.Text;

namespace TD3
{
    class Position
    {
        public double lat { get; set; }
        public double lng { get; set; }

        public Position(double lat, double lng)
        {
            this.lat = lat;
            this.lng = lng;
        }
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            foreach (System.Reflection.PropertyInfo property in this.GetType().GetProperties())
            {
                sb.Append(property.Name);
                sb.Append(": ");
                if (property.GetIndexParameters().Length > 0)
                {
                    sb.Append("Indexed Property cannot be used");
                }
                else
                {
                    sb.Append(property.GetValue(this, null));
                }

                sb.Append(System.Environment.NewLine);
            }

            return sb.ToString();
        }
    }
}
