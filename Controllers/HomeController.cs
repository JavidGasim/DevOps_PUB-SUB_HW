using System.Diagnostics;
using DevOps_PUB_SUB_HW.Models;
using DevOps_PUB_SUB_HW.Services.Abstracts;
using Microsoft.AspNetCore.Mvc;

namespace DevOps_PUB_SUB_HW.Controllers
{
    public class HomeController : Controller
    {
        private readonly IChannelService _channelService;

        public HomeController(IChannelService channelService)
        {
            _channelService = channelService;
        }

        [HttpGet]
        public async Task<IActionResult> Index(string selectedChannel = null)
        {
            var channels = await _channelService.GetChannels();
            ViewBag.Channels = channels;

            if (!string.IsNullOrEmpty(selectedChannel))
            {
                var messages = await _channelService.GetMessagesFromChannel(selectedChannel);
                ViewBag.SelectedMessages = messages;
                ViewBag.SelectedChannelName = selectedChannel;
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateChannel(string channelName)
        {
            await _channelService.CreateChannel(channelName);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> SelectChannel(string channelName)
        {
            var messages = await _channelService.GetMessagesFromChannel(channelName);
            ViewBag.SelectedMessages = messages;
            ViewBag.SelectedChannelName = channelName;

            var channels = await _channelService.GetChannels();
            ViewBag.Channels = channels;

            return View("Index");
        }

        [HttpPost]
        public async Task<IActionResult> SendMessage(string channelName, string message)
        {
            if (!string.IsNullOrEmpty(channelName) && channelName.Length != 0 && !string.IsNullOrEmpty(message) && message.Length != 0)
            {
                await _channelService.AddMessage(channelName, message);
            }

            return RedirectToAction("Index", new { selectedChannel = channelName });
        }

    }
}
