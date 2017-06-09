﻿namespace Bot.Web.Controllers
{
    using System;
    using System.Web.Http;
    using System.Web.Http.Description;
    using Bot.Core.Entities;

    public class InfoController : ApiController
    {
        private readonly ApplicationContext _applicationContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="InfoController"/> class.
        /// </summary>
        public InfoController(ApplicationContext applicationContext)
        {
            _applicationContext = applicationContext ?? throw new ArgumentNullException(nameof(applicationContext));
        }

        /// <summary>
        /// Gets information about the application.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ResponseType(typeof(ApplicationContext))]
        public IHttpActionResult Get()
        {
            return Ok(_applicationContext);
        }
    }
}
