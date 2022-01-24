window.addEventListener("scroll", () => {
	let nav = document.querySelector("nav");
	let winPos = window.scrollY > 0;

	nav.classList.toggle("scroll-active", winPos);
	btnHome.classList.toggle("b2h-btn", winPos);
});