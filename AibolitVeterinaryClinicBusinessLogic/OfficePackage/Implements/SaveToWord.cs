using AibolitVeterinaryClinicBusinessLogic.OfficePackage.HelperEnums;
using AibolitVeterinaryClinicBusinessLogic.OfficePackage.HelperModels;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;

namespace AibolitVeterinaryClinicBusinessLogic.OfficePackage.Implements
{
    public class SaveToWord :AbstractSaveToWord
    {
        private WordprocessingDocument _wordDocument;
        private Body _docBody;
        private Table _table;
        private static JustificationValues GetJustificationValues(WordJustificationType type)
        {
            return type switch
            {
                WordJustificationType.Both => JustificationValues.Both,
                WordJustificationType.Center => JustificationValues.Center,
                _ => JustificationValues.Left,
            };
        }
        private static SectionProperties CreateSectionProperties()
        {
            var properties = new SectionProperties();
            var pageSize = new PageSize
            {
                Orient = PageOrientationValues.Portrait
            };
            properties.AppendChild(pageSize);
            return properties;
        }
        private static ParagraphProperties CreateParagraphProperties(WordTextProperties paragraphProperties)
        {
            if (paragraphProperties != null)
            {
                var properties = new ParagraphProperties();
                properties.AppendChild(new Justification()
                {
                    Val = GetJustificationValues(paragraphProperties.JustificationType)
                });
                properties.AppendChild(new SpacingBetweenLines
                {
                    LineRule = LineSpacingRuleValues.Auto
                });
                properties.AppendChild(new Indentation());
                var paragraphMarkRunProperties = new ParagraphMarkRunProperties();
                if (!string.IsNullOrEmpty(paragraphProperties.Size))
                {
                    paragraphMarkRunProperties.AppendChild(new FontSize
                    {
                        Val = paragraphProperties.Size
                    });
                }
                properties.AppendChild(paragraphMarkRunProperties);
                return properties;
            }
            return null;
        }
        protected override void CreateWord(WordInfo info)
        {
            _wordDocument = WordprocessingDocument.Create(info.FileName, WordprocessingDocumentType.Document);
            MainDocumentPart mainPart = _wordDocument.AddMainDocumentPart();
            mainPart.Document = new Document();
            _docBody = mainPart.Document.AppendChild(new Body());
        }
        protected override void CreateParagraph(WordParagraph paragraph)
        {
            if (paragraph != null)
            {
                var docParagraph = new Paragraph();

                docParagraph.AppendChild(CreateParagraphProperties(paragraph.TextProperties));
                foreach (var run in paragraph.Texts)
                {
                    var docRun = new Run();
                    var properties = new RunProperties();
                    properties.AppendChild(new FontSize { Val = run.Item2.Size });
                    if (run.Item2.Bold)
                    {
                        properties.AppendChild(new Bold());
                    }
                    docRun.AppendChild(properties);
                    docRun.AppendChild(new Text
                    {
                        Text = run.Item1,
                        Space = SpaceProcessingModeValues.Preserve
                    });
                    docParagraph.AppendChild(docRun);
                }
                _docBody.AppendChild(docParagraph);
            }
        }
        protected override void CreateTable(List<string> tableHeaderInfo)
        {
            _table = new Table();
            TableProperties tblProps = new TableProperties(
                new TableBorders(
                new TopBorder
                {
                    Val = new EnumValue<BorderValues>(BorderValues.Single),
                    Size = 10
                },
                new BottomBorder
                {
                    Val = new EnumValue<BorderValues>(BorderValues.Single),
                    Size = 10
                },
                new LeftBorder
                {
                    Val = new EnumValue<BorderValues>(BorderValues.Single),
                    Size = 10
                },
                new RightBorder
                {
                    Val = new EnumValue<BorderValues>(BorderValues.Single),
                    Size = 10
                },
                new InsideHorizontalBorder
                {
                    Val = new EnumValue<BorderValues>(BorderValues.Single),
                    Size = 10
                },
                new InsideVerticalBorder
                {
                    Val = new EnumValue<BorderValues>(BorderValues.Single),
                    Size = 10
                }));
            _table.AppendChild<TableProperties>(tblProps);
            _docBody.AppendChild(_table);
            TableRow tableRowHeader = new TableRow();
            foreach (string stringHeaderCell in tableHeaderInfo)
            {
                TableCell cellHeader = new TableCell();
                cellHeader.Append(new TableCellProperties(new TableCellWidth() { Type = TableWidthUnitValues.Auto }));
                cellHeader.Append(new Paragraph(new Run(new Text(stringHeaderCell))));
                tableRowHeader.Append(cellHeader);
            }
            _table.Append(tableRowHeader);
        }
        protected override void AddRow(List<string> tableRowInfo)
        {
            TableRow tableRow = new TableRow();
            foreach (string cell in tableRowInfo)
            {
                TableCell tableCell = new TableCell();
                tableCell.Append(new Paragraph(new Run(new Text(cell))));
                tableRow.Append(tableCell);
            }
            _table.Append(tableRow);
        }
        protected override void SaveWord(WordInfo info)
        {
            _docBody.AppendChild(CreateSectionProperties());
            _wordDocument.MainDocumentPart.Document.Save();
            _wordDocument.Close();
        }
    }
}
