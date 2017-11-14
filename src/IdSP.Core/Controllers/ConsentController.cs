using IdentityServer4.Services;
using IdentityServer4.Stores;
using IdSP.Core.Models;
using IdSP.Core.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace IdSP.Core.Controllers
{
    /// <summary>
    /// This controller processes the consent UI
    /// </summary>
    [SecurityHeaders]
    public class ConsentController : Controller
    {
        private readonly ConsentService _consent;
        private readonly IConsentService _consentService;
        private readonly IIdentityServerInteractionService _interaction;
        private readonly IClientStore _clientStore;

        public ConsentController(
            IIdentityServerInteractionService interaction,
            IClientStore clientStore,
            IResourceStore resourceStore,
            IConsentService consentService,
            ILogger<ConsentController> logger)
        {
            _interaction = interaction;
            _clientStore = clientStore;
            _consentService = consentService;

            _consent = new ConsentService(interaction, clientStore, consentService, resourceStore, logger);
        }

        /// <summary>
        /// Shows the consent screen
        /// </summary>
        /// <param name="returnUrl"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Index(string returnUrl)
        {
            //            var requiresConsent = true;
            //
            //            var request = await _interaction.GetAuthorizationContextAsync(returnUrl);
            //            if (request != null)
            //            {
            //                var client = await _clientStore.FindEnabledClientByIdAsync(request.ClientId);
            //                if (client != null)
            //                {
            //                    requiresConsent = await _consentService.RequiresConsentAsync(User, client, request.ScopesRequested);
            //                }
            //            }
            //
            //            if (requiresConsent)
            //            {
            var vm = await _consent.BuildViewModelAsync(returnUrl);


            if (vm != null)
            {
                return View("Index", vm);
            }
            //            }
            //            else
            //            {
            //                return Redirect(returnUrl);
            //            }

            return View("Error");
        }

        /// <summary>
        /// Handles the consent screen postback
        /// </summary>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(ConsentInputModel model)
        {
            var result = await _consent.ProcessConsent(model);

            if (result.IsRedirect)
            {
                return Redirect(result.RedirectUri);
            }

            if (result.HasValidationError)
            {
                ModelState.AddModelError("", result.ValidationError);
            }

            if (result.ShowView)
            {
                return View("Index", result.ViewModel);
            }

            return View("Error");
        }
    }
}