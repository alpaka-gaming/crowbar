namespace Core.GameModel.SourceGlobal
{
    using System;

    public class SourceVector
    {
        public SourceVector()
        {
            x = 0;
            y = 0;
            z = 0;
        }

        public SourceVector(double iX, double iY, double iZ)
        {
            x = iX;
            y = iY;
            z = iZ;
        }

        public double x;
        public double y;
        public double z;

        public string debug_text;

        public SourceVector CrossProduct(SourceVector otherVector)
        {
            SourceVector crossVector = new SourceVector();

            crossVector.x = this.y * otherVector.z - this.z * otherVector.y;
            crossVector.y = this.z * otherVector.x - this.x * otherVector.z;
            crossVector.z = this.x * otherVector.y - this.y * otherVector.x;

            return crossVector;
        }

        public SourceVector Normalize()
        {
            double magnitude;
            SourceVector normalVector = new SourceVector();

            magnitude = Math.Sqrt(this.x * this.x + this.y * this.y + this.z * this.z);
            normalVector.x = this.x / magnitude;
            normalVector.y = this.y / magnitude;
            normalVector.z = this.z / magnitude;

            return normalVector;
        }
    }
}
