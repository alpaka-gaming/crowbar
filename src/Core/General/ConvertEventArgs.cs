using System;

namespace Core.General
{
    public class ConvertEventArgs : EventArgs
    {
        private object value;
        private System.Type desiredType;

        /// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ConvertEventArgs" /> class.</summary>
        /// <param name="value">An <see cref="T:System.Object" /> that contains the value of the current property.</param>
        /// <param name="desiredType">The <see cref="T:System.Type" /> of the value.</param>
        public ConvertEventArgs(object value, System.Type desiredType)
        {
            this.value = value;
            this.desiredType = desiredType;
        }

        /// <summary>Gets or sets the value of the <see cref="T:System.Windows.Forms.ConvertEventArgs" />.</summary>
        /// <returns>The value of the <see cref="T:System.Windows.Forms.ConvertEventArgs" />.</returns>
        public object Value
        {
            get => this.value;
            set => this.value = value;
        }

        /// <summary>Gets the data type of the desired value.</summary>
        /// <returns>The <see cref="T:System.Type" /> of the desired value.</returns>
        public System.Type DesiredType => this.desiredType;
    }
}
