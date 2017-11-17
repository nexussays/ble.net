using System;

namespace nexus.protocols.ble.gatt
{
   /// <summary>
   /// Utility methods for dealing with GATT
   /// </summary>
   public static class BleGattUtils
   {
      /// <summary>
      /// Return true if <see cref="CharacteristicProperty.Indicate" /> is set on this characteristic
      /// </summary>
      public static Boolean CanIndicate( this CharacteristicProperty properties )
      {
         return (properties & CharacteristicProperty.Indicate) != 0;
      }

      /// <summary>
      /// Return true if <see cref="CharacteristicProperty.Notify" /> is set on this characteristic
      /// </summary>
      public static Boolean CanNotify( this CharacteristicProperty properties )
      {
         return (properties & CharacteristicProperty.Notify) != 0;
      }

      /// <summary>
      /// Return true if <see cref="CharacteristicProperty.Read" /> is set on this characteristic
      /// </summary>
      public static Boolean CanRead( this CharacteristicProperty properties )
      {
         return (properties & CharacteristicProperty.Read) != 0;
      }

      /// <summary>
      /// Return true if <see cref="CharacteristicProperty.Write" /> or <see cref="CharacteristicProperty.WriteNoResponse" /> are
      /// set on this characteristic
      /// </summary>
      public static Boolean CanWrite( this CharacteristicProperty properties )
      {
         return (properties & (CharacteristicProperty.Write | CharacteristicProperty.WriteNoResponse)) != 0;
      }
   }
}