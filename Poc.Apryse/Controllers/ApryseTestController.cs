using AprysePoc.FileHelper;
using Microsoft.AspNetCore.Mvc;
using pdftron;
using pdftron.Filters;
using pdftron.PDF;
using pdftron.SDF;
using Poc.Apryse.Requests;
using static pdftron.PDF.PDFDoc;
using System.Drawing;
using static pdftron.PDF.Stamper;
using pdftron.PDF.PDFA;

namespace AprysePoc.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ApryseTestController : ControllerBase
    {
        const string APRYSE_KEY = "demo:1708083272960:7f5d1bf503000000001cec61b4f616d37b3355d8063ad749126b422897";

        const string HTML_MANIFEST = "<!DOCTYPE html>\r\n<html>\r\n\r\n<head>\r\n  <meta charset=\"utf-8\" />\r\n  <title></title>\r\n\r\n  <style>\r\n    html {\r\n      height: 100%;\r\n      width: 100vw;\r\n      margin: 0;\r\n    }\r\n\r\n    body {\r\n      padding: 10px 50px 50px 50px;\r\n      font-weight: normal;\r\n      font-family: \"Roboto\", sans-serif;\r\n      font-size: 16px;\r\n      font-weight: normal;\r\n      margin-bottom: 200px;\r\n      color: #363636;\r\n    }\r\n\t\r\n\tb{ \r\n\t font-weight: bold !important; \r\n\t} \r\n\r\n    header {\r\n      margin-top: 10px;\r\n    }\r\n\r\n    header>table {\r\n      width: 100%;\r\n    }\r\n\r\n    table>tbody>tr>td {\r\n      vertical-align: middle;\r\n    }\r\n\r\n    table>tbody>tr>td:last-child {\r\n      text-align: left;\r\n    }\r\n\r\n    table>tbody>tr>td>h2 {\r\n      margin: 0;\r\n    }\r\n\r\n    h2 {\r\n      margin-bottom: 2px;\r\n    }\r\n\r\n    section {\r\n      width: 100%;\r\n    }\r\n\r\n    label {\r\n      margin-bottom: 1rem;\r\n    }\r\n\r\n    .list-signatures {\r\n      display: -webkit-box;\r\n      display: -webkit-flex;\r\n      display: flex;\r\n      align-items: flex-end;\r\n      -webkit-box-orient: horizontal;\r\n      -webkit-box-align: end;\r\n    }\r\n\r\n    .signature {\r\n      -webkit-box-flex: 1;\r\n      -webkit-flex: 1;\r\n      flex: 1;\r\n      margin-right: 20px;\r\n      text-align: center;\r\n      max-width: 200px;\r\n    }\r\n\t\r\n\t.signature2 {\r\n      -webkit-box-flex: 1;\r\n      -webkit-flex: 1;\r\n      flex: 1;\r\n      margin-right: 10%;\r\n      text-align: center;\r\n      max-width: 200px;\r\n    }\r\n\r\n    .signature>img {\r\n      width: 100%;\r\n      text-align: center;\r\n      margin: 0 auto;\r\n    }\r\n\t\r\n\t.signature2>img {\r\n      text-align: center;\r\n      margin: 0 auto;\r\n    }\r\n\r\n    .totvs-lines {\r\n      position: fixed;\r\n      width: 100%;\r\n      left: 0;\r\n      right: 0;\r\n    }\r\n\r\n     .linha-assinatura {\r\n\t  border: 1px solid #363636;\r\n          margin: 1px 0;\r\n\t\t  width: 200px;\r\n    }\r\n\r\n    .top {\r\n      top: 20px;\r\n      z-index: -1;\r\n    }\r\n\r\n    .divider {\r\n      border: 1.5px solid #363636;\r\n      margin: 4px 0;\r\n    }\r\n\r\n    .mt-1 {\r\n      margin-top: 0.5rem;\r\n    }\r\n\r\n    .mt-3 {\r\n      margin-top: 1.2rem;\r\n    }\r\n\r\n    .mb-1 {\r\n      margin-bottom: 0.3rem;\r\n    }\r\n\r\n    .mr-2 {\r\n      margin-right: 2rem;\r\n    }\r\n\t\r\n\t.bloco-page-break {\r\n\t  page-break-inside: avoid;\r\n\t}\r\n\r\n  </style>\r\n\r\n  <link rel=\"stylesheet\" href=\"https://cdnjs.cloudflare.com/ajax/libs/font-awesome/5.15.2/css/all.min.css\" />\r\n</head>\r\n\r\n<body>\r\n  <header>\r\n    <table>\r\n      <tr>\r\n        <td>\r\n          <h1><strong>Protocolo de assinaturas</strog>\r\n          </h1>\r\n        </td>\r\n      </tr>\r\n    </table>\r\n  </header>\r\n\r\n  <section>\r\n    <h2>Documento</h2>\r\n    <div class=\"divider\"></div>\r\n    <div class=\"mt-3\">\r\n\t\t<div class=\"mb-1\">\r\n\t\t\t<strong>Documento gerado em ambiente de desenvolvimento - SEM VALIDADE LEGAL!</strong> \r\n\t\t</div>\r\n\t\t<div class=\"mb-1\">\r\n\t\t\t<strong>Nome do envelope:</strong> testeApryseDocumentoCorrompido2\r\n\t\t</div>\r\n\t\t<div class=\"mb-1\">\r\n\t\t\t<strong>Autor:</strong> ThiagoDev - thiago.mmelo+teste@totvs.com.br\r\n\t\t</div>\r\n\t\t<div class=\"mb-1\">\r\n\t\t\t<strong>Status:</strong> Assinado parcialmente\r\n\t\t</div>\r\n\t\t<div class=\"mb-1\">\r\n\t\t\t<strong>Hash:</strong> 5C-A3-C6-A6-9A-7E-19-C8-3B-18-43-64-66-70-10-13-ED-98-59-22\r\n\t\t</div>\r\n\t\t<div class=\"mb-1\">\r\n    <strong>Hash SHA256:</strong> fa7e917c674625fae097454427f629d73c926235d429f2689cd9ab3f23ca95da\r\n</div>\r\n\t</div>\r\n  </section><section>\r\n    <h2>Assinaturas</h2>\r\n    <div class=\"divider\"></div>\r\n    <div class=\" mt-3\">\r\n\t\t\t<div class=\"bloco-page-break\"><div class=\"mb-1\">\r\n        <strong>Nome:</strong> ThiagoDev - <strong>ID Internacional:</strong> 123 - <strong>Cargo:</strong> 123\r\n      </div><div class=\"mb-1\">\r\n        <strong>E-mail:</strong> thiago.mmelo+teste@totvs.com.br - <strong>Data:</strong> 19/02/2024 10:36:46\r\n      </div><div class=\"mb-1\">\r\n        <strong>Status:</strong> Assinado eletronicamente \r\n      </div><div class=\"mb-1\">\r\n        <strong>Tipo de Autenticação:</strong> Utilizando login e senha, pessoal e intransferível\r\n      </div><div class=\"mb-1\">\r\n        <strong>IP:</strong> 187.127.161.5 - <strong>IPV6:</strong> 2804:d45:3582:9d00:3cf7:c36d:906c:345\r\n      </div><div class=\"mb-1\">\r\n        <strong>Geolocalização:</strong>  Indisponível ou compartilhamento não autorizado pelo assinante\r\n      </div></div><br /><div class=\"bloco-page-break\"><div class=\"mb-1\">\r\n        <strong>Nome:</strong> Thiago Mariano de Melo - <strong>CPF/CNPJ:</strong> 397.024.768-36 - <strong>Cargo:</strong> PositionTest\r\n      </div><div class=\"mb-1\">\r\n        <strong>E-mail:</strong> thiago.mmelo@totvs.com.br - <strong>Data:</strong> 20/02/2024 11:43:05\r\n      </div><div class=\"mb-1\">\r\n        <strong>Status:</strong> Assinado eletronicamente \r\n      </div><div class=\"mb-1\">\r\n        <strong>Tipo de Autenticação:</strong> Utilizando login e senha, pessoal e intransferível\r\n      </div><div class=\"mb-1\">\r\n        <strong>Visualizado em:</strong> 20/02/2024 11:42:59 - <strong>Leitura completa em:</strong>\r\n        20/02/2024 11:43:02\r\n      </div><div class=\"mb-1\">\r\n        <strong>IP:</strong> 187.127.161.5 - <strong>IPV6:</strong> 2804:d45:3582:9d00:1cee:d8af:37df:eb9e\r\n      </div><div class=\"mb-1\">\r\n        <strong>Geolocalização:</strong> -19.922944, -43.9320576\r\n      </div><div class='list-signatures mt-1'>\r\n\t\t<div class='signature'>\r\n\t\t\t<img height ='40' src = 'data:image/png;base64, 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'>\r\n\t\t\t<div class='linha-assinatura'></div>\r\n\t\t\t<b>Assinatura</b>\r\n\t\t</div>\r\n\t\t<div class='signature2'>\r\n\t\t\t<img height ='40' src = 'data:image/png;base64, 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'>\r\n\t\t\t<div class='linha-assinatura'></div>\r\n\t\t\t<b>Rubrica</b>\r\n\t\t</div>\r\n\t  </div></div><br />\r\n\r\n    </div>\r\n  </section><section>\r\n    <h2>Pendentes</h2>\r\n    <div class=\"divider\"></div>\r\n    <div class=\"mt-3\">\r\n      <div class=\"bloco-page-break\"><div class=\"mb-1 \">\r\n        <strong>thiago.mmelo+213124@totvs.com.br</strong>\r\n      </div><div class=\"mb-1\">\r\n        <strong>Ação:</strong> Assinar\r\n      </div><div class=\"mb-1\">\r\n        <strong>Status:</strong> Ainda não visualizou o documento.\r\n      </div></div><br />\r\n    </div>\r\n  </section><section class='bloco-page-break'>\r\n    <h2>Autenticidade</h2>\r\n    <div class=\"divider\"></div>\r\n\r\n    <div class=\"mt-1\">\r\n      <p>Para verificar a autenticidade do documento, escaneie o QR Code ou acesse o link abaixo: </p>\r\n      <p><a target=\"_blank\" href=\"https://totvssign.dev.totvs.app/webapptotvssign/#/verify/search?codigo=5C-A3-C6-A6-9A-7E-19-C8-3B-18-43-64-66-70-10-13-ED-98-59-22\">https://totvssign.dev.totvs.app/webapptotvssign/#/verify/search?codigo=5C-A3-C6-A6-9A-7E-19-C8-3B-18-43-64-66-70-10-13-ED-98-59-22</a></p>\r\n      <p>Código HASH: 5C-A3-C6-A6-9A-7E-19-C8-3B-18-43-64-66-70-10-13-ED-98-59-22</p>\r\n      <img width=\"135\" src=\"data:image/png;base64, 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\" />\r\n    </div>\r\n  </section>\r\n\r\n</body>\r\n\r\n</html>";

        public ApryseTestController()
        {

            try
            {
                Console.WriteLine("**LOCAL CONSOLE** Initializing Apryse lib.");

                PDFNet.Initialize(APRYSE_KEY);

                Console.WriteLine("**LOCAL CONSOLE** Apryse Lib successfully initialized.");

            }
            catch (Exception ex)
            {
                Console.WriteLine($"**LOCAL CONSOLE** Unhandled exception while initializing Apryse lib: {ex.Message}");
            }
        }

        [HttpPost("Open")]
        public IActionResult OpenFile(IFormFile formFile)
        {
            try
            {
                var doc = new PDFDoc(formFile.GetBytesFromFormFile(), formFile.GetBytesFromFormFile().Length);
                //Open using bytes and bytes lenght from formFile

                if (!doc.InitSecurityHandler()) //Verify if is ecrypted
                    return new BadRequestObjectResult("ERROR! Document is encrypted");

                var bytesResult = doc.Save(SDFDoc.SaveOptions.e_linearized);

                doc.Close();

                return new OkObjectResult(bytesResult); //Return bytes

            }
            catch (Exception ex)
            {

                Console.WriteLine($"**LOCAL CONSOLE** Unhandled exception while Opening File: {ex.Message}");
            }

            return new OkResult();
        }

        [HttpPost("Merge")]
        public IActionResult MergeFiles([FromForm] CombinarArquivosRequest request)
        {
            try
            {
                var resultMergedDocument = new PDFDoc(); //Empty Doc

                foreach (var file in request.ArquivosPdf)
                {
                    using var document = new PDFDoc(file.GetBytesFromFormFile(), file.GetBytesFromFormFile().Length);
                    //Open using bytes and bytes lenght from formFile

                    if (!document.InitSecurityHandler()) //Verify if is ecrypted
                        return new BadRequestObjectResult("ERROR! Document is encrypted");

                    resultMergedDocument.InsertPages(resultMergedDocument.GetPageCount() + 1,
                            document, 1, document.GetPageCount(), PDFDoc.InsertFlag.e_none);
                    //InsertPages Parameters:
                    //Berfore x Pages
                    //Source document
                    //Source start page
                    //Source last page
                    //InsertFlag -> BookMarks or none
                }

                resultMergedDocument.Save($"./ArquivosGeradosLocalTest/{request.NomeArquivo}.pdf", SDFDoc.SaveOptions.e_incremental);
                //Save local to check changes

                return new OkObjectResult(resultMergedDocument.Save(SDFDoc.SaveOptions.e_incremental)); //Return bytes
            }
            catch (Exception ex)
            {
                Console.WriteLine($"**LOCAL CONSOLE** Unhandled exception while Merging Files: {ex.Message}");
            }

            return new OkResult();
        }


        [HttpPost("AddHash")]
        public IActionResult AddHash(IFormFile formFile)
        {
            try
            {
                var doc = new PDFDoc(formFile.GetBytesFromFormFile(), formFile.GetBytesFromFormFile().Length);

                if (!doc.InitSecurityHandler()) //Verify if is ecrypted
                    return new BadRequestObjectResult("ERROR! Document is encrypted");

                var stamper = new Stamper(SizeType.e_relative_scale, 0.55, 0.55); //Define text size

                stamper.SetPosition(-0.20, 0.48, true); //true -> percentage
                                                        //false -> pixel

                //Using default aligment cant add margin
                //stamper.SetAlignment(HorizontalAlignment.e_horizontal_left, VerticalAlignment.e_vertical_top);

                stamper.SetFont(pdftron.PDF.Font.Create(doc, pdftron.PDF.Font.StandardType1Font.e_courier, true)); //Can change Font
                stamper.SetFontColor(new ColorPt(0, 0, 0, 0)); //Color black

                var hash = "CÓDIGO: D9-60-6A-33-B5-56-F0-11-90-9C-42-22-25-7D-40-C6-9F-E5-BB-54";
                stamper.StampText(doc, hash, new PageSet(1, doc.GetPageCount())); //PageSet define range pages

                doc.Save("./ArquivosGeradosLocalTest/AddHashTest.pdf", SDFDoc.SaveOptions.e_incremental);
                //Save local to check changes

                return new OkObjectResult(doc.Save(SDFDoc.SaveOptions.e_incremental)); //Return bytes
            }
            catch (Exception ex)
            {
                Console.WriteLine($"**LOCAL CONSOLE** Unhandled exception while Adding Hash to File Pages: {ex.Message}");
            }

            return new OkResult();
        }


        [HttpPost("AddRubric")]
        public IActionResult AddRubric(IFormFile formFile, int qtdeRubricas)
        {
            try
            {
                var doc = new PDFDoc(formFile.GetBytesFromFormFile(), formFile.GetBytesFromFormFile().Length);

                if (!doc.InitSecurityHandler()) //Verify if is ecrypted
                    return new BadRequestObjectResult("ERROR! Document is encrypted");

                var stamper = new Stamper(SizeType.e_absolute_size, 300, 500); //Define text size

                stamper.SetPosition(-0.35, -0.45, true); //true -> percentage
                                                         //false -> pixel

                ////Using default aligment cant add margin
                //stamper.SetAlignment(HorizontalAlignment.e_horizontal_left, VerticalAlignment.e_vertical_bottom);

                //var multipleImages = System.IO.File.ReadAllBytes("./RubricTest.png");
                var multipleImages = GetRubricas(qtdeRubricas);
                //Using Rubric png Test

                var img = pdftron.PDF.Image.Create(doc, multipleImages); //Can create Image from bytes or local path

                stamper.SetAsBackground(false); // set image stamp as foreground

                stamper.StampImage(doc, img, new PageSet(1, doc.GetPageCount())); //Stamp Image

                doc.Save("./ArquivosGeradosLocalTest/AddRubricTest.pdf", SDFDoc.SaveOptions.e_incremental);
                //Save local to check changes

                return new OkObjectResult(doc.Save(SDFDoc.SaveOptions.e_incremental)); //Return bytes
            }
            catch (Exception ex)
            {
                Console.WriteLine($"**LOCAL CONSOLE** Unhandled exception while Adding Rubric to File Pages: {ex.Message}");
            }

            return new OkResult();
        }


        [HttpPost("ConvertMSOfficeToPdf")]
        public IActionResult ConvertMSOfficeToPdf(IFormFile formFile)
        {
            try
            {
                //Converting .doc, .docx, PPT and Excel

                byte[] docConverted;

                var bytes = formFile.GetBytesFromFormFile(); //Get bytes

                using (var memoryFilter = new MemoryFilter(bytes.Length, false)) //Create memoryfilter
                {
                    var writer = new FilterWriter(memoryFilter); //Create filterWriter to write the bytes on memory

                    writer.WriteBuffer(bytes); //Write bytes to the buffer memory
                    writer.Flush();

                    memoryFilter.SetAsInputFilter();

                    var options = new ConversionOptions();

                    var documentConversion = pdftron.PDF.Convert.StreamingPDFConversion(memoryFilter, options);

                    while (documentConversion.GetConversionStatus() == DocumentConversionResult.e_document_conversion_incomplete)
                    {
                        documentConversion.ConvertNextPage();
                    }
                    if (documentConversion.GetConversionStatus() == DocumentConversionResult.e_document_conversion_success)
                    {
                        documentConversion.GetDoc().Save("./ArquivosGeradosLocalTest/ConvertToPDF.pdf", SDFDoc.SaveOptions.e_linearized);
                        //Save local to check changes

                        docConverted = documentConversion.GetDoc().Save(SDFDoc.SaveOptions.e_linearized);
                        //Return the bytes
                    }
                    else
                    {
                        return new BadRequestObjectResult("Conversion FAILED");
                    }

                }

                return new OkObjectResult(docConverted); //Return bytes
            }
            catch (Exception ex)
            {
                Console.WriteLine($"**LOCAL CONSOLE** Unhandled exception while Adding Rubric to File Pages: {ex.Message}");
            }

            return new OkResult();
        }

        [HttpPost("ConvertToPDFA")]
        public IActionResult ConvertToPDFA(IFormFile formFile)
        {
            try
            {

                PDFNet.SetColorManagement(PDFNet.CMSType.e_lcms);

                var doc = new PDFDoc(formFile.GetBytesFromFormFile(), formFile.GetBytesFromFormFile().Length);

                var docBinary = doc.Save(SDFDoc.SaveOptions.e_incremental);

                using var pdf_a = new PDFACompliance(true, docBinary, null, PDFACompliance.Conformance.e_Level3B, null, 10, false);

                pdf_a.SaveAs("./ArquivosGeradosLocalTest/ConvertToPDFA.pdf", false);
                //Save local to check changes

                return new OkObjectResult(pdf_a.SaveAs(false)); //Return bytes
            }
            catch (Exception ex)
            {
                Console.WriteLine($"**LOCAL CONSOLE** Unhandled exception while Adding Rubric to File Pages: {ex.Message}");
            }

            return new OkResult();
        }


        [HttpPost("AddManifest")]
        public IActionResult AddManifest(IFormFile formFile)
        {
            try
            {
                var doc = new PDFDoc(formFile.GetBytesFromFormFile(), formFile.GetBytesFromFormFile().Length);

                if (!doc.InitSecurityHandler()) //Verify if is ecrypted
                    return new BadRequestObjectResult("ERROR! Document is encrypted");

                var docConverted = new PDFDoc();
                var converter = new HTML2PDF();

                converter.InsertFromHtmlString(HTML_MANIFEST);

                converter.Convert(docConverted);

                doc.InsertPages(doc.GetPageCount() + 1, docConverted, 1, docConverted.GetPageCount(), InsertFlag.e_none);

                doc.Save("./AddManifestPageTest.pdf", SDFDoc.SaveOptions.e_incremental); //Save local to check changes

                var base64Pdf = System.Convert.ToBase64String(doc.Save(SDFDoc.SaveOptions.e_incremental));

                return new OkObjectResult(base64Pdf); //Return bsae64
            }
            catch (Exception ex)
            {
                Console.WriteLine($"**LOCAL CONSOLE** Unhandled exception while Adding Manifest Page to File: {ex.Message}");
            }

            return new OkResult();
        }

        private byte[] GetRubricas(int qtdeRubricas)
        {
            var width = 0;
            var height = 0;
            var bitmapList = new List<Bitmap>();
            var imageBytes = System.IO.File.ReadAllBytes("./RubricTest.png");

            for (int i = 0; i < qtdeRubricas; i++)
            {
                using var stream = new MemoryStream(imageBytes);
                var image = new Bitmap(stream);
                width += image.Width;
                height = image.Height > height ? image.Height : height;
                bitmapList.Add(image);
            }

            var combinedBitmaps = new Bitmap(width + qtdeRubricas * 10, height);

            using (var g = Graphics.FromImage(combinedBitmaps))
            {
                int offset = 0;
                foreach (var imagem in bitmapList)
                {
                    g.DrawImage(imagem, new Rectangle(offset, 0, imagem.Width, imagem.Height));
                    offset += 10 + imagem.Width;
                }
            }

            using (var stream = new MemoryStream())
            {
                combinedBitmaps.Save(stream, System.Drawing.Imaging.ImageFormat.Png);
                return stream.ToArray();
            }
        }
    }
}
