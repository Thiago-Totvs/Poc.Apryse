using AprysePoc.FileHelper;
using Microsoft.AspNetCore.Mvc;
using pdftron.PDF;
using pdftron.SDF;
using Poc.Apryse.Requests;
using static pdftron.PDF.Stamper;

namespace AprysePoc.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ApryseTestController : ControllerBase
    {
        const string APRYSE_KEY = "demo:1708083272960:7f5d1bf503000000001cec61b4f616d37b3355d8063ad749126b422897";

        public ApryseTestController()
        {

            try
            {
                Console.WriteLine("**LOCAL CONSOLE** Initializing Apryse lib.");

                pdftron.PDFNet.Initialize(APRYSE_KEY);

                Console.WriteLine("**LOCAL CONSOLE** Foxit Lib successfully initialized.");

            }
            catch (Exception ex)
            {
                Console.WriteLine($"**LOCAL CONSOLE** Unhandled exception while initializing Apryse lib: {ex.Message}");
            }
        }

        [HttpPost("open")]
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

        [HttpPost("merge")]
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
            catch(Exception ex)
            {
                Console.WriteLine($"**LOCAL CONSOLE** Unhandled exception while Merging Files: {ex.Message}");
            }

            return new OkResult();
        }


        [HttpPost("addhash")]
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

                stamper.SetFont(Font.Create(doc, Font.StandardType1Font.e_courier, true)); //Can change Font
                stamper.SetFontColor(new ColorPt(0, 0, 0, 0)); //Color black

                var hash = "C�DIGO: D9-60-6A-33-B5-56-F0-11-90-9C-42-22-25-7D-40-C6-9F-E5-BB-54";
                stamper.StampText(doc, hash, new PageSet(1, doc.GetPageCount())); //PageSet define range pages

                doc.Save("./teste.pdf", SDFDoc.SaveOptions.e_incremental); //Save local to check changes

                return new OkObjectResult(doc.Save(SDFDoc.SaveOptions.e_incremental)); //Return bytes
            }
            catch (Exception ex)
            {
                Console.WriteLine($"**LOCAL CONSOLE** Unhandled exception while Adding Hash to File: {ex.Message}");
            }

            return new OkResult();
        }
    }
}
