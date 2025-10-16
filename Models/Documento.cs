    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    namespace FacturacionElectronicaSV.Models
    {
        public class Documento
        {
            [Key]
            public int IdDocumento { get; set; }

            [Required]
            [StringLength(2)]
            public string TipoDTE { get; set; } // Ej: "01" = FCF, "03" = Crédito Fiscal, "05" = Exportación

            [Required]
            public Guid CodigoGeneracion { get; set; }

            [Required]
            [StringLength(20)]
            public string NumeroControl { get; set; }

            [Required]
            [DataType(DataType.Date)]
            public DateTime FechaEmision { get; set; }

            [Required]
            [StringLength(5)]
            public string FormaPago { get; set; } // Ej: "01" = Contado, "02" = Crédito

            [Required]
            [Column(TypeName = "decimal(18,2)")]
            public decimal SubTotal { get; set; }

            [Required]
            [Column(TypeName = "decimal(18,2)")]
            public decimal TotalGravada { get; set; }

            [Required]
            [Column(TypeName = "decimal(18,2)")]
            public decimal TotalIVA { get; set; }

            [Required]
            [Column(TypeName = "decimal(18,2)")]
            public decimal TotalPagar { get; set; }

            [Required]
            [StringLength(200)]
            public string TotalLetras { get; set; }

            public bool EliminadoDTE { get; set; } = false;

            // Relaciones
            [ForeignKey("Emisor")]
            public int IdEmisor { get; set; }
            public Emisor Emisor { get; set; }

            [ForeignKey("Receptor")]
            public int IdReceptor { get; set; }
            public Receptor Receptor { get; set; }
        }
    }
